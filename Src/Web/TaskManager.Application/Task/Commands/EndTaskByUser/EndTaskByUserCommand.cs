namespace TaskManager.Application.Task.Commands.EndTaskByUser
{
    using FluentValidation;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    using TaskManager.Application.Interfaces;
    using TaskManager.Common.Exceptions;
    using TaskManager.Domain.Entity;

    public class EndTaskByUserCommand : IRequest
    {
        public string ApplicationUserId { get; set; }

        public int TaskId { get; set; }

        public class Handler : IRequestHandler<EndTaskByUserCommand>
        {
            private readonly IRepository<ToDoTask> taskRepository;

            private readonly INotificationService notificationService;

            public Handler(
                IRepository<ToDoTask> taskRepository,
                INotificationService notificationService)
            {
                this.taskRepository = taskRepository;
                this.notificationService = notificationService;
            }

            public async Task<Unit> Handle(EndTaskByUserCommand request, CancellationToken cancellationToken)
            {
                await new EndTaskByUserCommandValidator().ValidateAndThrowAsync(request);

                var task = await this.taskRepository.GetByIdAsync(request.TaskId)
                    ?? throw new EntityNotFoundException();

                await new EndTaskByUserCommandValidator(task).ValidateAndThrowAsync(request);

                if (task.ApplicationUserId == request.ApplicationUserId)
                {
                    task.IsDeleted = true;
                    this.taskRepository.Update(task);
                    await this.taskRepository.SaveAsync(cancellationToken);
                    await this.notificationService.SendMessageToAll($"Task {task.Id} - {task.Name} został zakończony");

                    return Unit.Value;
                }
                else
                {
                    throw new InvalidUserException();
                }
            }
        }
    }
}
