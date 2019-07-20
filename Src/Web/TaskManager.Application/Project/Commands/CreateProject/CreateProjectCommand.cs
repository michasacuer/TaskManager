namespace TaskManager.Application.Commands.CreateProject
{
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation;
    using MediatR;
    using TaskManager.Application.Interfaces;
    using TaskManager.Domain.Entity;

    public class CreateProjectCommand : IRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public class Handler : IRequestHandler<CreateProjectCommand>
        {
            private readonly IProjectRepository projectRepository;

            public Handler(IProjectRepository projectRepository)
            {
                this.projectRepository = projectRepository;
            }

            public async Task<Unit> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
            {
                await new CreateProjectCommandValidator().ValidateAndThrowAsync(request);

                await this.projectRepository.AddAsync(new Project
                {
                    Name = request.Name,
                    Description = request.Description
                });

                await this.projectRepository.SaveAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
