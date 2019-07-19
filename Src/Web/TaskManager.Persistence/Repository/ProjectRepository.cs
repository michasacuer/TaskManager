namespace TaskManager.Persistence.Repository
{
    using TaskManager.Domain.Entity;
    using TaskManager.Application.Interfaces;

    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(TaskManagerDbContext context)
            : base(context)
        {
        }
    }
}
