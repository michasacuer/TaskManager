namespace TaskManager.WPF.Helpers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TaskManager.Contracts.Data;
    using TaskManager.Entity;
    using TaskManager.WPF.Models;
    using TaskManager.WPF.ViewModels.Helper;

    public class TaskManagerHelper
    {
        private ProjectContract projectContract;

        private TaskContract taskContract;

        public TaskManagerHelper()
        {
            string bearer = LoggedUser.Instance.User.Bearer;

            this.projectContract = new ProjectContract(bearer);
            this.taskContract = new TaskContract(bearer);
        }

        public async Task<List<Project>> GetAllProjectsFromDatabase() => await this.projectContract.GetAllAsync();

        public async Task<ToDoTask> GetTaskToActivate(ToDoTask selectedTask)
        {
            bool isSucceed = await this.taskContract.TakeTaskByUserAsync(selectedTask.Id, LoggedUser.Instance.User.Id);
            if (isSucceed)
            {
                ApplicationWindows.ShowSuccesBox($"Task {selectedTask.Name} przypisano do {LoggedUser.Instance.GetUserFullName()}!");

                return await this.taskContract.GetUsersTask(LoggedUser.Instance.User.Id);
            }
            else
            {
                ApplicationWindows.ShowErrorBox("Błąd przy przypisywaniu taska!");

                return default;
            }
        }
    }
}
