namespace TaskManager.Interfaces
{
    using System.Threading.Tasks;
    using TaskManager.Entity;

    public interface ITasks : IBaseContracts<ToDoTask>
    {
        Task<ToDoTask> GetUsersTask(string userId);

        Task<ToDoTask> EndActiveTaskByUser(ToDoTask task);
    }
}
