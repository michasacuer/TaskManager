namespace TaskManager.Persistence.Repository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using TaskManager.Application.Interfaces;
    using TaskManager.Domain.Entity;

    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(TaskManagerDbContext context)
            : base(context)
        {
        }

        public async Task<List<Project>> GetAllProjectsWithTasksAsync()
        {
            return await base.context.Projects.Include(p => p.Tasks).ToListAsync();
        }

        public async Task<Project> GetProjectWithTasksAsync(int id)
        {
            return await base.context.Projects
                .Include(p => p.Tasks)
                .Include(p => p.EndedTasks)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
