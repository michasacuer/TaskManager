namespace TaskManager.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TaskManager.Entity;

    public interface INotifications
    {
        Task<List<Notification>> GetAllAsync();
    }
}
