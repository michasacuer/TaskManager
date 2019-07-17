namespace TaskManager.Application.Task.Commands.TakeTaskByUser
{
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using TaskManager.Application.Interfaces;
    using TaskManager.Common.Exceptions;
    using TaskManager.Domain.Entity;

    public class TakeTaskByUserCommand : IRequest
    {
        public int TaskId { get; set; }

        public string ApplicationUserId { get; set; }

        public class Handler : IRequestHandler<TakeTaskByUserCommand>
        {
            private readonly ITaskManagerDbContext context;

            private readonly UserManager<ApplicationUser> userManager;

            public Handler(ITaskManagerDbContext context, UserManager<ApplicationUser> userManager)
            {
                this.context = context;
                this.userManager = userManager;
            }

            public async Task<Unit> Handle(TakeTaskByUserCommand request, CancellationToken cancellationToken)
            {
                await new TakeTaskByUserCommandValidator().ValidateAndThrowAsync(request);

                var user = await this.context.Users.FindAsync(request.ApplicationUserId)
                    ?? throw new UserNotFoundException();

                if (await this.userManager.IsInRoleAsync(user, "Manager") || await this.userManager.IsInRoleAsync(user, "Developer"))
                {
                    var task = await this.context.Tasks.FindAsync(request.TaskId)
                      ?? throw new EntityNotFoundException();

                    await new TaskApplicationUserIdValidator().ValidateAndThrowAsync(task);

                    task.ApplicationUserId = user.Id;
                    this.context.Tasks.Update(task);

                    await this.context.SaveChangesAsync(cancellationToken);
                }
                else
                {
                    throw new UserHaveNoPermissionException();
                }

                return Unit.Value;
            }
        }
    }
}
