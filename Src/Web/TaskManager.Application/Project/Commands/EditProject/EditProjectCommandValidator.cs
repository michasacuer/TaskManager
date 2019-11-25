namespace TaskManager.Application.Commands.EditProject
{
    using FluentValidation;
    
    public class EditProjectCommandValidator : AbstractValidator<EditProjectCommand>
    {
        public EditProjectCommandValidator()
        {
            RuleFor(x => x.Project.Id).NotEmpty();
            RuleFor(x => x.Project.Name).NotEmpty();
        }
    }
}
