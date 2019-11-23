namespace TaskManager.Contracts.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TaskManager.Entity;

    public interface INotificationContract
    {
        Task<List<Notification>> GetAllAsync();
    }
}
