namespace TaskManager.Application.Task.Commands.TakeTaskByUser
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation;
    using MediatR;
    using TaskManager.Application.Interfaces;
    using TaskManager.Common.Exceptions;
    using TaskManager.Domain.Entity;

    public class TakeTaskByUserCommand : IRequest
    {
        public int TaskId { get; set; }

        public string ApplicationUserId { get; set; }

        public class Handler : IRequestHandler<TakeTaskByUserCommand>
        {
            private readonly IApplicationUserRepository applicationUserRepository;

            private readonly IRepository<ToDoTask> taskRepository;

            private readonly INotificationService notificationService;

            public Handler(
                IApplicationUserRepository applicationUserRepository,
                IRepository<ToDoTask> taskRepository,
                INotificationService notificationService)
            {
                this.applicationUserRepository = applicationUserRepository;
                this.taskRepository = taskRepository;
                this.notificationService = notificationService;
            }

            public async Task<Unit> Handle(TakeTaskByUserCommand request, CancellationToken cancellationToken)
            {
                await new TakeTaskByUserCommandValidator().ValidateAndThrowAsync(request);

                var user = await this.applicationUserRepository.GetByIdAsync(request.ApplicationUserId)
                    ?? throw new EntityNotFoundException();

                if (await this.applicationUserRepository.UserInRoleAsync(user, "Manager")
                    || await this.applicationUserRepository.UserInRoleAsync(user, "Developer"))
                {
                    var task = await this.taskRepository.GetByIdAsync(request.TaskId)
                        ?? throw new EntityNotFoundException();

                    await new TaskApplicationUserIdValidator().ValidateAndThrowAsync(task);

                    task.ApplicationUserId = user.Id;
                    task.StartTime = DateTime.Now;

                    this.taskRepository.Update(task);
                    await this.taskRepository.SaveAsync(cancellationToken);
                    await this.notificationService.SendMessageToAll($"{user.FirstName} {user.LastName} " +
                        $"zaczął zadanie {task.Id} = {task.Name}");

                    return Unit.Value;
                }
                else
                {
                    throw new UserHaveNoPermissionException();
                }
            }
        }
    }
}
