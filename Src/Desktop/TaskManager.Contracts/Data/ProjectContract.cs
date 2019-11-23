namespace TaskManager.Contracts.Data
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using TaskManager.Contracts.Extensions;
    using TaskManager.Contracts.Interfaces;
    using TaskManager.Entity;

    public class ProjectContract : IProjectService
    {
        private HttpClient httpClient;

        public ProjectContract()
        {
            this.httpClient = new HttpClient();
        }

        public async Task<Project> AddAsync(Project project) => await this.httpClient.PostAsync(project);

        public async Task<List<Project>> GetAllAsync() => await this.httpClient.GetAsync<Project>();

        public async Task<Project> GetAsync(int projectId) => await this.httpClient.GetAsync<Project>(projectId);
    }
}
