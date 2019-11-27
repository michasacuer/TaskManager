namespace TaskManager.WPF.Helpers
{
    using System.Threading.Tasks;
    using TaskManager.Contracts.Data;
    using TaskManager.Entity;
    using TaskManager.WPF.Models;

    public class ActiveTaskHelper
    {
        private TaskContract taskContract;

        public ActiveTaskHelper()
        {
            this.taskContract = new TaskContract(LoggedUser.Instance.User.Bearer);
        }

        public async Task<bool> EndTaskByUser(ToDoTask task) => await this.taskContract.EndActiveTaskByUserAsync(task.Id, task.ApplicationUserId);
    }
}
