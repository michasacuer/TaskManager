namespace TaskManager.Contracts.Data
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using TaskManager.Contracts.Extensions;
    using TaskManager.Contracts.Interfaces;
    using TaskManager.Entity;
    using TaskManager.Entity.JSONMapper;

    public class ProjectContract : BaseContract, IProjectContract
    {
        public ProjectContract(string bearer)
            : base(bearer)
        {
        }

        public async Task<Project> AddAsync(Project project) => await base.httpClient.PostAsync(project);

        public async Task<bool> EditAsync(Project data) => await base.httpClient.PostAsync(data, "Edit");

        public async Task<List<Project>> GetAllAsync() => await base.httpClient.GetAsync<Projects, Project>();

        public async Task<Project> GetAsync(int projectId) => await base.httpClient.GetAsync<Project>(projectId);

        public async Task<bool> DeleteAsync(int id) => await base.httpClient.DeleteAsync<Project>(id);
    }
}
