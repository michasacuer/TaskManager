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

            private readonly INotificationService notificationService;

            public Handler(IProjectRepository projectRepository, INotificationService notificationService)
            {
                this.projectRepository = projectRepository;
                this.notificationService = notificationService;
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
                await this.notificationService.SendMessageToAll($"Utworzono projekt {request.Name}");

                return Unit.Value;
            }
        }
    }
}
