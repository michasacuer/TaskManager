namespace TaskManager.Contracts.Data
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using TaskManager.Contracts.Extensions;
    using TaskManager.Contracts.Interfaces;
    using TaskManager.Entity;

    public class Notifications : INotifications
    {
        private HttpClient httpClient;

        public Notifications()
        {
            this.httpClient = new HttpClient();
        }

        public Task<List<Notification>> GetAllAsync() => this.httpClient.GetAsync<Notification>();
    }
}
