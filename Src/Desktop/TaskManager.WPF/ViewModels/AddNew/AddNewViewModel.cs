namespace TaskManager.WPF.ViewModels
{
    using Caliburn.Micro;
    using TaskManager.WPF.ViewModels.Helper;

    public class AddNewViewModel : Conductor<IScreen>.Collection.OneActive
    {
        public AddNewViewModel()
        {
            this.Items.Add(new AddNewTaskViewModel { DisplayName = "Zadanie" });
            this.Items.Add(new AddNewProjectViewModel { DisplayName = "Projekt" });
        }

        public void CancelButton() => this.TryCloseAsync();
    }
}
