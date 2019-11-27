namespace TaskManager.WPF.ViewModels
{
    using System.Collections.Generic;
    using Caliburn.Micro;
    using TaskManager.Entity;
    using TaskManager.WPF.Helpers;
    using TaskManager.WPF.Models;
    using TaskManager.WPF.ViewModels.Helper;

    public class TasksDataGridViewModel : Screen
    {
        private InfoBoxHelper helper;

        public TasksDataGridViewModel()
        {
            this.helper = new InfoBoxHelper();
        }

        public List<ToDoTask> Tasks { get; set; }

        public async void InfoButton(ToDoTask task)
        {
            ApplicationWindows.ShowInfoTaskBox(task);
            this.Tasks = await this.helper.GetAllTasksFromDatabase();
            this.NotifyOfPropertyChange(() => this.Tasks);
        }

        public async void DeleteButton(ToDoTask task)
        {
            if (LoggedUser.Instance.IsManager())
            {
                ApplicationWindows.ShowDeleteTaskBox(task);
                this.Tasks = await this.helper.GetAllTasksFromDatabase();
                this.NotifyOfPropertyChange(() => this.Tasks);
            }
            else
            {
                ApplicationWindows.ShowErrorBox("Nie masz uprawnień do usuwania zadań!");
            }
        }

        protected async override void OnViewLoaded(object view)
        {
            this.Tasks = await this.helper.GetAllTasksFromDatabase();
            this.NotifyOfPropertyChange(() => this.Tasks);
        }
    }
}
