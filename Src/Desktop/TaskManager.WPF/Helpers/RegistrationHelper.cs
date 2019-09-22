namespace TaskManager.WPF.Helpers
{
    using System.Threading.Tasks;
    using TaskManager.BindingModel;
    using TaskManager.Contracts.Account;
    using TaskManager.Validation;
    using TaskManager.Validation.Models;
    using TaskManager.WPF.ViewModels;

    public class RegistrationHelper
    {
        public async Task<ValidationResult> ExternalRegistrationAsync(RegistrationViewModel registrationViewModel)
        {
            var accountForm = new RegistrationBindingModel
            {
                UserName = registrationViewModel.LoginTextBox,
                Password = registrationViewModel.PasswordTextBox,
                FirstName = registrationViewModel.FirstNameTextBox,
                LastName = registrationViewModel.LastNameTextBox,
                Email = registrationViewModel.EmailTextBox,
                Role = registrationViewModel.Role
            };

            var validationResult = new RegistrationForm().IsValid(
                accountForm,
                registrationViewModel.ManagerChecked,
                registrationViewModel.DeveloperChecked,
                registrationViewModel.ViewerChecked);

            if (validationResult.IsValid)
            {
                var account = new Account();
                await account.RegisterAsync(accountForm);
            }

            return validationResult;
        }
    }
}
