namespace TaskManager.Application.Task.Commands.TakeTaskByUser
{
    using FluentValidation;
    using TaskManager.Domain.Entity;

    public class TakeTaskByUserCommandValidator : AbstractValidator<TakeTaskByUserCommand>
    {
        public TakeTaskByUserCommandValidator()
        {
            RuleFor(x => x.ApplicationUserId).NotEmpty();
        }
    }

    public class TaskApplicationUserIdValidator : AbstractValidator<Task>
    {
        public TaskApplicationUserIdValidator()
        {
            RuleFor(x => x.ApplicationUserId).Empty();
        }
    }
}
