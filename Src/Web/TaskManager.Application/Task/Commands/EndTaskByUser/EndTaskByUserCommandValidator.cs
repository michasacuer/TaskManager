namespace TaskManager.Application.Task.Commands.EndTaskByUser
{
    using FluentValidation;
    using TaskManager.Domain.Entity;

    public class EndTaskByUserCommandValidator : AbstractValidator<EndTaskByUserCommand>
    {
        public EndTaskByUserCommandValidator()
        {
            RuleFor(x => x.ApplicationUserId).NotEmpty();
            RuleFor(x => x.TaskId).NotEmpty();
        }

        public EndTaskByUserCommandValidator(ToDoTask task)
        {
            RuleFor(x => x.ApplicationUserId).Equal(task.ApplicationUserId);
        }
    }
}
