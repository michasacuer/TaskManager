namespace TaskManager.Application.Interfaces
{
    using System.Threading.Tasks;
    using TaskManager.Domain.Entity;

    public interface ITaskRepository : IRepository<ToDoTask>
    {
        Task<ToDoTask> GetUserTask(string userId);
    }
}
