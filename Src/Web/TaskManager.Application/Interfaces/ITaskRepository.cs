namespace TaskManager.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TaskManager.Domain.Entity;

    public interface ITaskRepository : IRepository<ToDoTask>
    {
        Task<ToDoTask> GetUserTask(string userId);

        Task<List<ToDoTask>> GetAllEndedTasksAsync();
    }
}
