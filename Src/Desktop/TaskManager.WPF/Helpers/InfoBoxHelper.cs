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

        private EndedTaskContract endedTaskContract;

        public InfoBoxHelper()
        {
            string bearer = LoggedUser.Instance.User.Bearer;

            this.projectContract = new ProjectContract(bearer);
            this.taskContract = new TaskContract(bearer);
            this.endedTaskContract = new EndedTaskContract(bearer);
        }

        public async Task<List<Project>> GetAllProjectsFromDatabase() => await this.projectContract.GetAllAsync();

        public async Task<List<ToDoTask>> GetAllTasksFromDatabase() => await this.taskContract.GetAllAsync();

        public async Task<List<EndedTask>> GetAllEndedTasksFromDatabase() => await this.endedTaskContract.GetAllAsync();

        public async Task<bool> EditTask(ToDoTask task) => await this.taskContract.EditAsync(task);

        public async Task<bool> EditProject(Project project) => await this.projectContract.EditAsync(project);
    }
}
