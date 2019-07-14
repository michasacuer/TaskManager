namespace TaskManager.Application.Task.Commands.CreateTask
{
    using FluentValidation;

    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.ProjectId).NotEmpty();
        }
    }
}
