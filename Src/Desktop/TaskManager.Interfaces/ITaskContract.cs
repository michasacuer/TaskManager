namespace TaskManager.Contracts.Interfaces
{
    using System.Threading.Tasks;
    using TaskManager.Entity;

    public interface ITaskContract : IBaseContract<ToDoTask>
    {
        Task<ToDoTask> GetUsersTask(string userId);

        Task<bool> EndActiveTaskByUserAsync(int taskId, string userId);

        Task<bool> TakeTaskByUserAsync(int taskId, string userId);
    }
}
