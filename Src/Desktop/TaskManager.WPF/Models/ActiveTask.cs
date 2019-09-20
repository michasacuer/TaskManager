namespace TaskManager.WPF.Models
{
    using System.Threading.Tasks;
    using TaskManager.Contracts.Data;
    using TaskManager.Entity;

    public class ActiveTask
    {
        public ToDoTask Task { get; set; }

        public void AttackTask(ToDoTask task) => this.Task = task;

        public bool IsTaskTakenByUser() => this.Task != null;

        public async Task<bool> IfUserHaveActiveTask(string userId)
        {
            var task = await new Tasks().GetUsersTask(userId);

            if (task != null)
            {
                this.Task = task;
            }
            else
            {
                return false;
            }

            return true;
        }

        public async Task EndActiveUsersTask()
        {
            await new Tasks().EndActiveTaskByUser(this.Task);
            this.Task = null;
        }
    }
}
