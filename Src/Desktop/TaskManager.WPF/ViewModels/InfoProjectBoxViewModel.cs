namespace TaskManager.WPF.ViewModels
{
    using Caliburn.Micro;
    using TaskManager.Entity;
    using TaskManager.WPF.Helpers;
    using TaskManager.WPF.Models;
    using TaskManager.WPF.ViewModels.Helper;

    public class InfoProjectBoxViewModel : Screen
    {
        private Project project;

        private InfoBoxHelper helper;

        public InfoProjectBoxViewModel(Project project)
        {
            this.helper = new InfoBoxHelper();

            this.project = project;
            this.NavbarProjectName = project.Name;
            this.IdTextBox = project.Id.ToString();
            this.NameTextBox = project.Name;
            this.DescriptionTextBox = project.Description;
        }

        public string NavbarProjectName { get; set; }

        public string IdTextBox { get; set; }

        public string NameTextBox { get; set; }

        public string DescriptionTextBox { get; set; }

        public void SaveButton()
        {
            if (LoggedUser.Instance.IsManager())
            {
                this.project.Name = this.NameTextBox;
                this.project.Description = this.DescriptionTextBox;

                this.helper.EditProject(this.project);

                ApplicationWindows.ShowSuccesBox("Pomyślnie edytowano projekt.");
                this.TryCloseAsync();
            }
            else
            {
                ApplicationWindows.ShowErrorBox("Nie masz uprawnień do edytowania projektów!");
            }
        }

        public void CancelButton() => this.TryCloseAsync();
    }
}
