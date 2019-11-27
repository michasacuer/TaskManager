namespace TaskManager.WPF.ViewModels
{
    using System.Collections.Generic;
    using Caliburn.Micro;
    using TaskManager.Entity;
    using TaskManager.WPF.Helpers;

    public class EndedTasksDataGridViewModel : Screen
    {
        private InfoBoxHelper helper;

        public EndedTasksDataGridViewModel()
        {
            this.helper = new InfoBoxHelper();
        }

        public List<EndedTask> EndedTasks { get; set; }

        protected async override void OnViewLoaded(object view)
        {
            this.EndedTasks = await this.helper.GetAllEndedTasksFromDatabase();
            this.NotifyOfPropertyChange(() => this.EndedTasks);
        }
    }
}
