namespace TaskManager.Infrastructure.Hubs
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR;

    public class NotificationHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await this.Clients.Caller.SendAsync("ReciveMessage", message);
        }
    }
}
