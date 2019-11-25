namespace TaskManager.Application.Commands.EditProject
{
    using FluentValidation;
    
    public class EditProjectCommandValidator : AbstractValidator<EditProjectCommand>
    {
        public EditProjectCommandValidator()
        {
            RuleFor(x => x.Data.Id).NotEmpty();
            RuleFor(x => x.Data.Name).NotEmpty();
        }
    }
}
