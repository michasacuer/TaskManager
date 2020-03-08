namespace TaskManager.Persistence.Repository
{
    using System.Collections.Generic;
    using System.Linq;
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

        public async Task<List<ToDoTask>> GetAllEndedTasksAsync()
        {
            return await base.context.Tasks.Where(t => t.IsDeleted).AsNoTracking().ToListAsync();
        }

        public async Task<ToDoTask> GetUserTask(string userId)
        {
           return await base.context.Tasks.FirstOrDefaultAsync(t => t.ApplicationUserId == userId);
        }
    }
}
