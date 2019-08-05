namespace TaskManager.Application.Task.Commands.TakeTaskByUser
{
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

            public Handler(IApplicationUserRepository applicationUserRepository, IRepository<ToDoTask> taskRepository)
            {
                this.applicationUserRepository = applicationUserRepository;
                this.taskRepository = taskRepository;
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
                    this.taskRepository.Update(task);
                    await this.taskRepository.SaveAsync(cancellationToken);

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
