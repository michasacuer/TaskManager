namespace TaskManager.Contracts.Hub
{
    using Microsoft.AspNetCore.SignalR.Client;

    public class NotificationsHub
    {
        public static NotificationsHub Instance { get; } = new NotificationsHub();

        public async void Initialize()
        {
            var hubConnection = new HubConnectionBuilder()
                .WithUrl(UrlBuilder.BuildEndpoint("Notifications"))
                .Build();

            hubConnection.On<string>("ReciveServerUpdate", async message =>
            {
                //todo data retriving, setting vm etc
            });

            await hubConnection.StartAsync();
        }
    }
}
