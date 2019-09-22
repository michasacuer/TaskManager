namespace TaskManager.WPF.ViewModels
{
    using Caliburn.Micro;
    using TaskManager.Contracts.Exceptions;
    using TaskManager.Entity.Enum;
    using TaskManager.WPF.Helpers;
    using TaskManager.WPF.ViewModels.Helper;

    public class RegistrationViewModel : Screen
    {
        public bool IsFormEnabled { get; set; } = true;

        public string LoginTextBox { get; set; } = "Wpisz swój Login";

        public string PasswordTextBox { get; set; } = "Hasło";

        public string FirstNameTextBox { get; set; } = "Wpisz swoje Imie";

        public string LastNameTextBox { get; set; } = "Wpisz swoje Nazwisko";

        public string EmailTextBox { get; set; } = "Wpisz swój Email";

        public Role Role { get; set; }

        public bool ManagerChecked { get; set; }

        public bool DeveloperChecked { get; set; }

        public bool ViewerChecked { get; set; }

        public async void AcceptButton()
        {
            this.IsFormEnabled = false;
            this.NotifyOfPropertyChange(() => this.IsFormEnabled);

            var helper = new RegistrationHelper();

            try
            {
                var validationResult = await helper.ExternalRegistrationAsync(this);

                if (validationResult.IsValid)
                {
                    await this.TryCloseAsync();
                    ApplicationWindows.ShowSuccesBox(validationResult.Message);
                }
                else
                {
                    ApplicationWindows.ShowErrorBox(validationResult.Message);

                    this.IsFormEnabled = true;
                    this.NotifyOfPropertyChange(() => this.IsFormEnabled);
                }
            }
            catch (RegistrationException exception)
            {
                ApplicationWindows.ShowErrorBox(exception.Message);

                this.IsFormEnabled = true;
                this.NotifyOfPropertyChange(() => this.IsFormEnabled);
            }
        }

        public void CancelButton() => this.TryCloseAsync();
    }
}
