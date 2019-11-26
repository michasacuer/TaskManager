namespace TaskManager.Application.Task.Commands.EditTask
{
    using FluentValidation;

    public class EditTaskCommandValidator : AbstractValidator<EditTaskCommand>
    {
        public EditTaskCommandValidator()
        {
            RuleFor(x => x.Data.Id).NotEmpty();
            RuleFor(x => x.Data.Name).NotEmpty();
        }
    }
}
