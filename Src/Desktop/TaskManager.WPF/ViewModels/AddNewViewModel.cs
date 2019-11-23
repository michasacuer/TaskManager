namespace TaskManager.WPF.ViewModels
{
    using Caliburn.Micro;

    public class AddNewViewModel : Conductor<IScreen>.Collection.OneActive
    {
        public AddNewViewModel()
        {
            this.Items.Add(new AddNewProjectViewModel { DisplayName = "Projekt" });
        }

        public void CancelButton() => this.TryCloseAsync();
    }
}
