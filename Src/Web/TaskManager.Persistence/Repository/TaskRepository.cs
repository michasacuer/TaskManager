namespace TaskManager.Persistence.Repository
{
    using TaskManager.Application.Interfaces;
    using TaskManager.Domain.Entity;

    public class TaskRepository : Repository<Task>, ITaskRepository
    {
        public TaskRepository(TaskManagerDbContext context)
            : base(context)
        {
        }
    }
}
