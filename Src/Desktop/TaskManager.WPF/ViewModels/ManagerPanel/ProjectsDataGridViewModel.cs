namespace TaskManager.WPF.ViewModels
{
    using System.Collections.Generic;
    using Caliburn.Micro;
    using TaskManager.Entity;
    using TaskManager.WPF.Helpers;
    using TaskManager.WPF.Models;
    using TaskManager.WPF.ViewModels.Helper;

    public class ProjectsDataGridViewModel : Screen
    {
        private InfoBoxHelper helper;

        public ProjectsDataGridViewModel()
        {
            this.helper = new InfoBoxHelper();
        }

        public List<Project> Projects { get; set; }

        public async void InfoButton(Project project)
        {
            ApplicationWindows.ShowInfoProjectBox(project);
            this.Projects = await this.helper.GetAllProjectsFromDatabase();
            this.NotifyOfPropertyChange(() => this.Projects);
        }

        public async void DeleteButton(Project project)
        {
            if (LoggedUser.Instance.IsManager())
            {
                ApplicationWindows.ShowDeleteProjectBox(project);
                
                this.Projects = await this.helper.GetAllProjectsFromDatabase();
                this.NotifyOfPropertyChange(() => this.Projects);
            }
            else
            {
                ApplicationWindows.ShowErrorBox("Nie masz uprawnień do usuwania zadań!");
            }
        }

        protected async override void OnViewLoaded(object view)
        {
            this.Projects = await this.helper.GetAllProjectsFromDatabase();
            this.NotifyOfPropertyChange(() => this.Projects);
        }
    }
}