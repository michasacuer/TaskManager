namespace TaskManager.WPF.ViewModels
{
    using Caliburn.Micro;
    using System.Windows;
    using TaskManager.WPF.Models;
    using TaskManager.WPF.ViewModels.Helper;

    class UserInfoViewModel : Screen
    {
        private MainWindowViewModel mainWindowViewModel;

        public UserInfoViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            this.LoggedUserFullName = LoggedUser.Instance.GetUserFullName();
            this.LoggedUserJob = LoggedUser.Instance.GetUserPosition();

            this.NotifyOfPropertyChange(() => this.LoggedUserJob);
            this.NotifyOfPropertyChange(() => this.LoggedUserFullName);
        }

        public string LoggedUserFullName { get; set; }

        public string LoggedUserJob { get; set; }

        public void LogoutButton()
        {
            this.mainWindowViewModel.IsActiveTaskButtonVisible = Visibility.Hidden;
            this.mainWindowViewModel.NotifyOfPropertyChange(() => this.mainWindowViewModel.IsActiveTaskButtonVisible);

            LoggedUser.Instance.Logout();
            this.TryCloseAsync();

            ApplicationWindows.ShowLoginBox(this.mainWindowViewModel);
        }

        public void LoadManagerPanel() => ApplicationWindows.ShowManagerPanelBox();

        public void OkButton() => this.TryCloseAsync();

        public void ExitButton() => Application.Current.Shutdown();
    }
}
