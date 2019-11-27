namespace TaskManager.WPF.ViewModels
{
    using Caliburn.Micro;
    using TaskManager.Entity;
    using TaskManager.WPF.Helpers;
    using TaskManager.WPF.ViewModels.Helper;

    public class DeleteProjectBoxViewModel : Screen
    {
        private DeleteFromDatabaseHelper helper;

        private Project projectToDelete;

        public DeleteProjectBoxViewModel(Project project)
        {
            this.helper = new DeleteFromDatabaseHelper();

            this.projectToDelete = project;
            this.DeleteName = project.Name;
        }

        public string DeleteName { get; set; }

        public async void YesButton()
        {
            bool isSucceed = await this.helper.DeleteProject(projectToDelete.Id);
            if (isSucceed)
            {
                ApplicationWindows.ShowSuccesBox($"Projekt {DeleteName} usunięto pomyślnie!");
            }
            else
            {
                ApplicationWindows.ShowErrorBox("Błąd przy usuwaniu zadania!");
            }

            this.TryCloseAsync();
        }

        public void NoButton() => this.TryCloseAsync();
    }
}
