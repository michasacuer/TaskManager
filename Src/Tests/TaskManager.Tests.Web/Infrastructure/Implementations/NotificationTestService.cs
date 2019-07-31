namespace TaskManager.Tests.Web.Infrastructure.Implementations
{
    using System.Threading.Tasks;
    using TaskManager.Application.Interfaces;
    using TaskManager.Domain.Entity;

    public class NotificationTestService : INotificationService
    {
        private readonly IRepository<Notification> repository;

        public NotificationTestService(IRepository<Notification> repository)
        {
            this.repository = repository;
        }

        public async Task SendMessageToAll(string message)
        {
            await repository.AddAsync(new Notification
            {
                Message = message
            });

            await this.repository.SaveAsync();
        }
    }
}
