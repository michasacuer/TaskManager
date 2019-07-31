namespace TaskManager.Tests.Web.Infrastructure.Hubs
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR;

    public class NotificationTestHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await this.Clients.Caller.SendAsync("ReciveMessage", message);
        }
    }
}
