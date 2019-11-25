namespace TaskManager.WPF.Helpers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TaskManager.Contracts.Data;
    using TaskManager.Entity;
    using TaskManager.WPF.Models;

    public class InfoBoxHelper
    {
        private ProjectContract projectContract;

        private TaskContract taskContract;

        public InfoBoxHelper()
        {
            string bearer = LoggedUser.Instance.User.Bearer;

            this.projectContract = new ProjectContract(bearer);
            this.taskContract = new TaskContract(bearer);
        }

        public async Task<List<Project>> GetAllProjectsFromDatabase() => await this.projectContract.GetAllAsync();

        public async Task<bool> EditTask(ToDoTask task) => await this.EditTaskInDatabase(task, task.Id);

        public async Task<bool> EditProject(Project project) => await this.EditProjectInDatabase(project, project.Id);

        private async Task<bool> EditProjectInDatabase(Project project, int itemId) => await this.projectContract.EditAsync(project);

        private async Task<bool> EditTaskInDatabase(ToDoTask task, int itemId) => await this.taskContract.EditAsync(task);
    }
}
