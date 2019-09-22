namespace TaskManager.Contracts.Data
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using TaskManager.Contracts.Extensions;
    using TaskManager.Contracts.Interfaces;
    using TaskManager.Entity;

    public class Projects : IProjects
    {
        private HttpClient httpClient;

        public Projects()
        {
            this.httpClient = new HttpClient();
        }

        public async Task<List<Project>> GetAllAsync() => await this.httpClient.GetAsync<Project>();

        public async Task<Project> GetAsync(int projectId) => await this.httpClient.GetAsync<Project>(projectId);
    }
}
