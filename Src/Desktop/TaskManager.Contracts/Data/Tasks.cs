﻿namespace TaskManager.Contracts.Data
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using TaskManager.BindingModel.Commands;
    using TaskManager.Contracts.Extensions;
    using TaskManager.Contracts.Interfaces;
    using TaskManager.Entity;

    public class Tasks : ITasks
    {
        private HttpClient httpClient;

        public Tasks()
        {
            this.httpClient = new HttpClient();
        }

        public async Task<ToDoTask> EndActiveTaskByUser(ToDoTask task, string userId)
            => await this.httpClient.PostAsync(
                task, 
                new EndTaskByUserBindingModel { ApplicationUserId = userId, TaskId = task.Id }, 
                "EndTask");

        public async Task<List<ToDoTask>> GetAllAsync() => await this.httpClient.GetAsync<ToDoTask>();

        public async Task<ToDoTask> GetAsync(int taskId) => await this.httpClient.GetAsync<ToDoTask>(taskId);

        public async Task<ToDoTask> GetUsersTask(string userId) => await this.httpClient.GetAsync<ToDoTask>(userId);
    }
}
