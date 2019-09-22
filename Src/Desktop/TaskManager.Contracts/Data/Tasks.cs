namespace TaskManager.Contracts.Data
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using TaskManager.BindingModel.Commands;
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

        public async Task<ToDoTask> EndActiveTaskByUser(ToDoTask task, string userId)
            => await this.httpClient.PostAsync(
                Consts.BaseUrl,
                task, 
                new EndTaskByUserBindingModel { ApplicationUserId = userId, TaskId = task.Id }, 
                "EndTask");

        public async Task<List<ToDoTask>> GetAllAsync() => await this.httpClient.GetAsync<ToDoTask>(Consts.BaseUrl);

        public async Task<ToDoTask> GetAsync(int taskId) => await this.httpClient.GetAsync<ToDoTask>(Consts.BaseUrl, taskId);

        public async Task<ToDoTask> GetUsersTask(string userId) => await this.httpClient.GetAsync<ToDoTask>(Consts.BaseUrl, userId);
    }
}
