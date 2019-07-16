namespace TaskManager.Application.Task.Commands.AssignTaskToUser
{
    using FluentValidation;
    using TaskManager.Domain.Entity;

    public class AssignTaskToUserValidator : AbstractValidator<AssignTaskToUserCommand>
    {
        public AssignTaskToUserValidator()
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
