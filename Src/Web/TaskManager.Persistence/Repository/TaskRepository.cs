namespace TaskManager.Persistence.Repository
{
    using TaskManager.Application.Interfaces;
    using TaskManager.Domain.Entity;

    public class TaskRepository : Repository<ToDoTask>, ITaskRepository
    {
        public TaskRepository(TaskManagerDbContext context)
            : base(context)
        {
        }
    }
}
