namespace TaskManager.WPF.ViewModels
{
    using System.Collections.Generic;
    using Caliburn.Micro;
    using TaskManager.WPF.Helpers;

    public class NotificationsViewModel : Screen
    {
        public NotificationsViewModel()
        {
            NotificationsHubService.Instance.SetReferenceToViewModel(this);
        }

        public BindableCollection<string> Notifications { get; set; }

        public void CancelButton()
        {
            NotificationsHubService.Instance.SetViewModelToNull();
            this.TryCloseAsync();
        }

        protected async override void OnViewLoaded(object view)
        {
            this.Notifications = new BindableCollection<string>();

            var notifications = await NotificationsHubService.Instance.NotificationContract.GetAllAsync();
            foreach (var notification in notifications)
            {
                this.Notifications.Add(notification.Message);
            }

            this.NotifyOfPropertyChange(() => this.Notifications);
        }
    }
}
