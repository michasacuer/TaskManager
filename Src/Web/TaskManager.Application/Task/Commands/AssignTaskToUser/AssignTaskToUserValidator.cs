namespace TaskManager.Application.Task.Commands.AssignTaskToUser
{
    using FluentValidation;

    public class AssignTaskToUserValidator : AbstractValidator<AssignTaskToUserCommand>
    {
        public AssignTaskToUserValidator()
        {
            RuleFor(x => x.ApplicationUserId).NotEmpty();
        }
    }
}
