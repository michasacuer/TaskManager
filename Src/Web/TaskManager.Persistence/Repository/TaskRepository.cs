namespace TaskManager.Persistence.Repository
{
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using TaskManager.Application.Interfaces;
    using TaskManager.Domain.Entity;

    public class TaskRepository : Repository<ToDoTask>, ITaskRepository
    {
        public TaskRepository(TaskManagerDbContext context)
            : base(context)
        {
        }

        public async Task<ToDoTask> GetUserTask(string userId)
        {
           return await base.context.Tasks.FirstOrDefaultAsync(t => t.ApplicationUserId == userId);
        }
    }
}
