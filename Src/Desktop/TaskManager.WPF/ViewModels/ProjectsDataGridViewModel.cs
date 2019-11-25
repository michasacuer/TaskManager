namespace TaskManager.WPF.ViewModels
{
    using System.Collections.Generic;
    using Caliburn.Micro;
    using TaskManager.Entity;
    using TaskManager.WPF.Models;
    using TaskManager.WPF.ViewModels.Helper;

    public class ProjectsDataGridViewModel : Screen
    {
        public List<Project> Projects { get; set; }

        public void InfoButton(Project project)
        {
            //ApplicationWindows.ShowInfoProjectBox(project);

            
            this.NotifyOfPropertyChange(() => this.Projects);
        }

        public void DeleteButton(Project project)
        {
            if (LoggedUser.Instance.IsManager())
            {
                //ApplicationWindows.ShowDeleteProjectBox(project);
                //
                //this.Projects = (List<Project>)Repository.Instance.Projects;
                this.NotifyOfPropertyChange(() => this.Projects);
            }
            else
            {
                ApplicationWindows.ShowErrorBox("Nie masz uprawnień do usuwania zadań!");
            }
        }
    }
}