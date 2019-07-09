namespace TaskManager.Application.Commands
{
    using FluentValidation;

    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
