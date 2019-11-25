namespace TaskManager.Contracts.Data
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using TaskManager.BindingModel.Commands;
    using TaskManager.Contracts.Extensions;
    using TaskManager.Contracts.Interfaces;
    using TaskManager.Entity;
    using TaskManager.Entity.JSONMapper;

    public class TaskContract : BaseContract, ITaskContract
    {
        public TaskContract(string bearer)
            : base(bearer)
        {
        }

        public async Task<bool> EndActiveTaskByUser(ToDoTask task, string userId)
            => await this.httpClient.PostAsync<ToDoTask>(
                new EndTaskByUserBindingModel { ApplicationUserId = userId, TaskId = task.Id }, 
                "EndTask");

        public async Task<ToDoTask> AddAsync(ToDoTask task) => await base.httpClient.PostAsync(task);

        public async Task<List<ToDoTask>> GetAllAsync() => await base.httpClient.GetAsync<ToDoTasks, ToDoTask>();

        public async Task<ToDoTask> GetAsync(int taskId) => await base.httpClient.GetAsync<ToDoTask>(taskId);

        public async Task<ToDoTask> GetUsersTask(string userId) => await base.httpClient.GetAsync<ToDoTask>(userId);

        public async Task<bool> EditAsync(ToDoTask data) => await base.httpClient.PostAsync(data, "Edit");
    }
}
