namespace TaskManager.Application.Task.Commands
{
    using FluentValidation;

    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.ProjectId).GreaterThan(0);
            RuleFor(x => x.Priority).NotEmpty();
        }
    }
}
