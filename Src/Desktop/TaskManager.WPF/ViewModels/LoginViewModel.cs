namespace TaskManager.WPF.ViewModels
{
    using System.Windows;
    using Caliburn.Micro;
    using TaskManager.Contracts.Exceptions;
    using TaskManager.WPF.Helpers;
    using TaskManager.WPF.Models;
    using TaskManager.WPF.ViewModels.Helper;

    public class LoginViewModel : Screen
    {
        private MainWindowViewModel mainWindowViewModel;

        public LoginViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
        }

        public bool IsFormEnabled { get; set; } = true;

        public string LoginTextBox { get; set; }

        public string PasswordTextBox { get; set; }

        public async void LoginButton()
        {
            this.IsFormEnabled = false;
            this.NotifyOfPropertyChange(() => this.IsFormEnabled);

            var helper = new LoginHelper();

            try
            {
                var validationResult = await helper.ExternlLoginAsync(this.LoginTextBox, this.PasswordTextBox);

                if (validationResult.IsValid)
                {
                    await this.TryCloseAsync();
                    ApplicationWindows.ShowSuccesBox(validationResult.Message);

                    if (LoggedUser.Instance.GetUserTask().IsTaskTakenByUser())
                    {
                        this.mainWindowViewModel.IsActiveTaskButtonVisible = Visibility.Visible;
                        this.mainWindowViewModel.NotifyOfPropertyChange(() => this.mainWindowViewModel.IsActiveTaskButtonVisible);
                    }
                    else
                    {
                        ApplicationWindows.ShowErrorBox(validationResult.Message);

                        this.IsFormEnabled = true;
                        this.NotifyOfPropertyChange(() => this.IsFormEnabled);
                    }
                }
            }
            catch (LoginException exception)
            {
                ApplicationWindows.ShowErrorBox(exception.Message);

                this.IsFormEnabled = true;
                this.NotifyOfPropertyChange(() => this.IsFormEnabled);
            }
        }

        public void CancelButton() => Application.Current.Shutdown();
    }
}
