namespace TaskManager.Contracts.Interfaces
{
    using System.Threading.Tasks;
    using TaskManager.Entity;

    public interface ITaskContract : IBaseContract<ToDoTask>
    {
        Task<ToDoTask> GetUsersTask(string userId);

        Task<ToDoTask> EndActiveTaskByUser(ToDoTask task, string userId);
    }
}
