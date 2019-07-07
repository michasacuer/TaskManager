namespace TaskManager.Application.Commands
{
    using FluentValidation;

    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterUserCommandValidator()
        {
            this.RuleFor(x => x.UserName).NotEmpty();
            this.RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
            this.RuleFor(x => x.Email).NotEmpty().EmailAddress();
            this.RuleFor(x => x.FirstName).NotEmpty();
            this.RuleFor(x => x.LastName).NotEmpty();
        }
    }
}
