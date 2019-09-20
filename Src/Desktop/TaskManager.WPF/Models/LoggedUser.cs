namespace TaskManager.WPF.Models
{
    using TaskManager.Entity;
    using TaskManager.Entity.Enum;

    public class LoggedUser
    {
        private ActiveTask activeTask;

        public LoggedUser()
        {
            this.activeTask = new ActiveTask();
        }

        public static LoggedUser Instance { get; } = new LoggedUser();

        public ApplicationUser User { get; set; }

        public void AttachTaskToUser(ToDoTask task) => this.activeTask.AttackTask(task);

        public void LoginUserToApp(ApplicationUser user) => this.User = user;

        public string GetUserFullName() => $"{this.User.FirstName} {this.User.LastName}";

        public ActiveTask GetUserTask() => this.activeTask;

        public bool HavePermissionToTakeTask()
            => this.User.Role == Role.Manager || this.User.Role == Role.Developer;

        public bool IsManager() => this.User.Role == Role.Manager;

        public void Logout()
        {
            this.User = null;
            this.activeTask = new ActiveTask();
        }
    }
}
