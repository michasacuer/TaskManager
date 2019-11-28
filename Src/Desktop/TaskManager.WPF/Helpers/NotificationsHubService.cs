namespace TaskManager.WPF.Helpers
{
    using Microsoft.AspNetCore.SignalR.Client;
    using TaskManager.Contracts;
    using TaskManager.Contracts.Data;
    using TaskManager.WPF.Models;
    using TaskManager.WPF.ViewModels;

    public class NotificationsHubService
    {
        private NotificationsViewModel notificationsViewModel;

        public static NotificationsHubService Instance { get; } = new NotificationsHubService();

        public void SetReferenceToViewModel(NotificationsViewModel notificationsViewModel)
        {
            this.NotificationContract = new NotificationContract(LoggedUser.Instance.User.Bearer);
            this.notificationsViewModel = notificationsViewModel;
        }

        public NotificationContract NotificationContract { get; set; }

        public void SetViewModelToNull() => this.notificationsViewModel = null;

        public async void Initialize()
        {
            var hubConnection = new HubConnectionBuilder()
                .WithUrl(UrlBuilder.BuildEndpoint("Notifications"))
                .Build();

            hubConnection.On<string>("ReciveMessage", async message =>
            {
                if (this.notificationsViewModel != null)
                {
                    this.notificationsViewModel.Notifications.Add(message);
                    this.notificationsViewModel.NotifyOfPropertyChange(() => this.notificationsViewModel.Notifications);
                }
            });

            await hubConnection.StartAsync();
        }
    }
}
