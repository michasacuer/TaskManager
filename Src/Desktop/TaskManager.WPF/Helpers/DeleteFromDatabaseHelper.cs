namespace TaskManager.WPF.Helpers
{
    using System.Threading.Tasks;
    using TaskManager.Contracts.Data;
    using TaskManager.WPF.Models;

    public class DeleteFromDatabaseHelper
    {
        private ProjectContract projectContract;

        private TaskContract taskContract;

        public DeleteFromDatabaseHelper()
        {
            string bearer = LoggedUser.Instance.User.Bearer;

            this.projectContract = new ProjectContract(bearer);
            this.taskContract = new TaskContract(bearer);
        }

        public async Task<bool> DeleteProject(int projectId) => await this.projectContract.DeleteAsync(projectId);
        
        public async Task<bool> DeleteTask(int taskId) => await this.taskContract.DeleteAsync(taskId);
    }
}
