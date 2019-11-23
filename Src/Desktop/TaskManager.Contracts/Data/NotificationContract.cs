namespace TaskManager.Contracts.Data
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using TaskManager.Contracts.Extensions;
    using TaskManager.Contracts.Interfaces;
    using TaskManager.Entity;

    public class NotificationService : INotificationContract
    {
        private HttpClient httpClient;

        public NotificationService()
        {
            this.httpClient = new HttpClient();
        }

        public Task<List<Notification>> GetAllAsync() => this.httpClient.GetAsync<Notification>();
    }
}
