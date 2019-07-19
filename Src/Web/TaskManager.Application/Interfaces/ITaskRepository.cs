namespace TaskManager.Application.Interfaces
{
    using TaskManager.Domain.Entity;

    public interface ITaskRepository : IRepository<ToDoTask>
    {
    }
}
