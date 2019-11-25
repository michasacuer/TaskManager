namespace TaskManager.Application.Commands.EditProject
{
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation;
    using MediatR;
    using TaskManager.Application.Interfaces;
    using TaskManager.Common.Exceptions;
    using TaskManager.Domain.Entity;

    public class EditProjectCommand : IRequest
    {
        public Project Data { get; set; }

        public class Handler : IRequestHandler<EditProjectCommand>
        {
            private readonly IProjectRepository projectRepository;

            public Handler(IProjectRepository projectRepository)
            {
                this.projectRepository = projectRepository;
            }

            public async Task<Unit> Handle(EditProjectCommand request, CancellationToken cancellationToken)
            {
                await new EditProjectCommandValidator().ValidateAndThrowAsync(request);

                var project = await this.projectRepository.GetByIdAsync(request.Data.Id)
                    ?? throw new EntityNotFoundException();

                project.Name = request.Data.Name;
                project.Description = request.Data.Description;

                this.projectRepository.Update(project);
                await this.projectRepository.SaveAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
