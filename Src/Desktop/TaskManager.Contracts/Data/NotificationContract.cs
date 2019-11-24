namespace TaskManager.Contracts.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TaskManager.Contracts.Extensions;
    using TaskManager.Contracts.Interfaces;
    using TaskManager.Entity;
    using TaskManager.Entity.JSONMapper;

    public class NotificationContract : BaseContract, INotificationContract
    {
        public NotificationContract(string bearer)
            : base(bearer)
        {
        }

        public Task<List<Notification>> GetAllAsync() => this.httpClient.GetAsync<Notifications, Notification>();
    }
}
