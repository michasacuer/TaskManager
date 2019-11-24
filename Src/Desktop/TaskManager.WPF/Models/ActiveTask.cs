namespace TaskManager.WPF.Models
{
    using System.Threading.Tasks;
    using TaskManager.Contracts.Data;
    using TaskManager.Contracts.Exceptions;
    using TaskManager.Entity;

    public class ActiveTask
    {
        public ToDoTask Task { get; set; }

        public void AttackTask(ToDoTask task) => this.Task = task;

        public bool IsTaskTakenByUser() => this.Task != null;

        public async Task<bool> IsUserHaveActiveTask(string userId)
        {
            try
            {
                var task = await new TaskContract(LoggedUser.Instance.User.Bearer).GetUsersTask(userId);

                if (task != null)
                {
                    this.Task = task;
                }

                return true;
            }
            catch (NotFoundServerException)
            {
                return false;
            }
        }

        public async Task EndActiveUsersTask(string userId)
        {
            await new TaskContract(LoggedUser.Instance.User.Bearer).EndActiveTaskByUser(this.Task, userId);
            this.Task = null;
        }
    }
}
