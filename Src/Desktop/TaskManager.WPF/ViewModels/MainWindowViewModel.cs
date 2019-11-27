namespace TaskManager.WPF.ViewModels
{
    using System;
    using System.Windows;
    using Caliburn.Micro;
    using TaskManager.WPF.Models;
    using TaskManager.WPF.ViewModels.Helper;

    public class MainWindowViewModel : Conductor<IScreen>.Collection.OneActive
    {
        public Visibility IsActiveTaskButtonVisible { get; set; } = Visibility.Hidden;

        public void LoadUserInfoPage() => this.ActivateItemAsync(new UserInfoViewModel(this));

        public void LoadTaskManagerPage() => this.ActivateItemAsync(new TaskManagerViewModel(this));

        public void LoadAddNewPage() => this.ActivateItemAsync(new AddNewViewModel());

        public void LoadActiveTaskPage() => ApplicationWindows.ShowActiveTask(this);

        public void HideBar()
        {
            if (LoggedUser.Instance.GetUserTask().IsTaskTakenByUser())
            {
                this.IsActiveTaskButtonVisible = Visibility.Hidden;
                this.NotifyOfPropertyChange(() => this.IsActiveTaskButtonVisible);
            }
        }

        public void ShowBar()
        {
            if (LoggedUser.Instance.GetUserTask().IsTaskTakenByUser())
            {
                this.IsActiveTaskButtonVisible = Visibility.Visible;
                this.NotifyOfPropertyChange(() => this.IsActiveTaskButtonVisible);
            }
        }

        protected async override void OnViewLoaded(object view)
        {
            ApplicationWindows.ShowLoginBox(this);

            try
            {
                //todo signalr hub, fetching data
            }
            catch (NullReferenceException)
            {
                return;
            }
        }
    }
}
