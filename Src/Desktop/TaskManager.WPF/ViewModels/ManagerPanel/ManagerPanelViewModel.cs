namespace TaskManager.WPF.ViewModels
{
    using System.Collections.Generic;
    using Caliburn.Micro;
    using TaskManager.Entity;
    using TaskManager.WPF.Helpers;
    using TaskManager.WPF.ViewModels.Helper;

    public class ManagerPanelViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private ManagerPanelHelper helper;

        public ManagerPanelViewModel()
        {
            this.helper = new ManagerPanelHelper();

            this.Items.Add(new ProjectsDataGridViewModel { DisplayName = "Projekty" });
            this.Items.Add(new TasksDataGridViewModel { DisplayName = "Zadania" });
            this.Items.Add(new EndedTasksDataGridViewModel { DisplayName = "Skończone zadania" });
        }

        public List<Project> Projects { get; set; }

        public Project SelectedProject { get; set; }

        public async void GeneratePdfButton()
        {
            if (this.SelectedProject == null)
            {
                ApplicationWindows.ShowErrorBox("Wybierz projekt!");
                return;
            }

            var fileDialog = new FileDialog();
            string filepath = fileDialog.OpenSaveFile();

            if (filepath == string.Empty)
            {
                ApplicationWindows.ShowErrorBox("Wybierz nazwę projektu!");
                return;
            }

            filepath = await this.helper.GeneratePdf(this.SelectedProject, filepath);
            ApplicationWindows.ShowSuccesBox("PDF utworzono pomyślnie!");
            ApplicationWindows.ShowPdf(filepath);
        }

        public void PrintRaportButton()
        {
            if (this.SelectedProject == null)
            {
                ApplicationWindows.ShowErrorBox("Wybierz projekt!");
                return;
            }

            this.helper.PrintRaport(this.SelectedProject);
        }

        public void ExitButton() => this.TryCloseAsync();

        protected async override void OnViewLoaded(object view)
        {
            this.Projects = await this.helper.GetAllProjectsFromDatabase();
            this.NotifyOfPropertyChange(() => this.Projects);
        }
    }
}