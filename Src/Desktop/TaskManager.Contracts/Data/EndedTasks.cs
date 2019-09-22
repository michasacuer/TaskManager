namespace TaskManager.Contracts.Data
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using TaskManager.Contracts.Extensions;
    using TaskManager.Contracts.Interfaces;
    using TaskManager.Entity;

    public class EndedTasks : IEndedTasks
    {
        private HttpClient httpClient;

        public EndedTasks()
        {
            this.httpClient = new HttpClient();
        }

        public async Task<List<EndedTask>> GetAllAsync() => await this.httpClient.GetAsync<EndedTask>();

        public async Task<EndedTask> GetAsync(int taskId) => await this.httpClient.GetAsync<EndedTask>(taskId);
    }
}
