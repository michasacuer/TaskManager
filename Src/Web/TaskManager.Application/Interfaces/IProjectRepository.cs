namespace TaskManager.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TaskManager.Domain.Entity;

    public interface IProjectRepository : IRepository<Project>
    {
        Task<Project> GetProjectWithTasksAsync(int id);

        Task<List<Project>> GetAllProjectsWithTasksAsync();
    }
}
