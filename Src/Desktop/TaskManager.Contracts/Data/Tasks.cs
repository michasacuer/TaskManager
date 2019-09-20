namespace TaskManager.Contracts.Data
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using TaskManager.Contracts.Extensions;
    using TaskManager.Entity;
    using TaskManager.Interfaces;

    public class Tasks : ITasks
    {
        private HttpClient httpClient;

        public Tasks()
        {
            this.httpClient = new HttpClient();
        }

        public async Task<ToDoTask> EndActiveTaskByUser(ToDoTask task)
            => await this.httpClient.PutAsync(task, "End", task.Id.ToString(), task.ApplicationUserId.ToString());

        public async Task<List<ToDoTask>> GetAllAsync() => await this.httpClient.GetAsync<ToDoTask>();

        public async Task<ToDoTask> GetAsync(int taskId) => await this.httpClient.GetAsync<ToDoTask>(taskId);

        public async Task<ToDoTask> GetUsersTask(string userId) => await this.httpClient.GetAsync<ToDoTask>(userId);
    }
}
