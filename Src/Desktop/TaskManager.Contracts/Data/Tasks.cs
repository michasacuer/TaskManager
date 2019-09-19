namespace TaskManager.Contracts.Data
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using TaskManager.Contracts.Extensions;
    using TaskManager.Entity;

    public class Tasks
    {
        private HttpClient httpClient;

        public Tasks()
        {
            this.httpClient = new HttpClient();
        }

        public async Task<List<ToDoTask>> GetAllAsync() => await this.httpClient.GetAsync<ToDoTask>();

        public async Task<ToDoTask> GetAsync(int taskId) => await this.httpClient.GetAsync<ToDoTask>(taskId);
    }
}
