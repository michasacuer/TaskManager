namespace TaskManager.Infrastructure.Implementations
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR;
    using TaskManager.Application.Interfaces;
    using TaskManager.Domain.Entity;
    using TaskManager.Infrastructure.Hubs;

    public class NotificationService : INotificationService
    {
        private readonly IRepository<Notification> repository;

        private readonly IHubContext<NotificationHub> hubContext;

        public NotificationService(IRepository<Notification> repository, IHubContext<NotificationHub> hubContext)
        {
            this.repository = repository;
            this.hubContext = hubContext;
        }

        public async Task SendMessageToAll(string message)
        {
            await this.hubContext.Clients.All.SendAsync("ReciveMessage", message);

            await repository.AddAsync(new Notification
            {
                Message = message
            });

            await this.repository.SaveAsync();
        }
    }
}
