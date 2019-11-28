namespace TaskManager.Application.Task.Commands.CreateTask
{
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation;
    using MediatR;
    using TaskManager.Application.Interfaces;
    using TaskManager.Common.Exceptions;
    using TaskManager.Domain.Entity;
    using TaskManager.Domain.Enum;

    public class CreateTaskCommand : IRequest
    {
        public int ProjectId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Priority Priority { get; set; }

        public int? StoryPoints { get; set; }

        public class Handler : IRequestHandler<CreateTaskCommand>
        {
            private readonly IRepository<ToDoTask> taskRepository;

            private readonly IProjectRepository projectRepository;

            private readonly INotificationService notificationService;

            public Handler(
                IRepository<ToDoTask> taskRepository,
                IProjectRepository projectRepository,
                INotificationService notificationService)
            {
                this.taskRepository = taskRepository;
                this.projectRepository = projectRepository;
                this.notificationService = notificationService;
            }

            public async Task<Unit> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
            {
                await new CreateTaskCommandValidator().ValidateAndThrowAsync(request);

                var project = await this.projectRepository.GetByIdAsync(request.ProjectId)
                    ?? throw new EntityNotFoundException();

                var task = new ToDoTask
                {
                    Name = request.Name,
                    Description = request.Description,
                    Priority = request.Priority,
                    ProjectId = request.ProjectId,
                    StoryPoints = request.StoryPoints
                };

                await this.taskRepository.AddAsync(task);
                await this.taskRepository.SaveAsync(cancellationToken);
                await this.notificationService.SendMessageToAll($"Utworzono nowy task w projekcie {project.Name}");

                return Unit.Value;
            }
        }
    }
}
