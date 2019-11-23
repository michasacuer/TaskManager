namespace TaskManager.WPF.ViewModels
{
    using Caliburn.Micro;
    using TaskManager.WPF.Helpers;
    using TaskManager.WPF.ViewModels.Helper;

    public class AddNewProjectViewModel : Screen
    {
        public string ProjectNameTextBox { get; set; }

        public string DescriptionTextBox { get; set; }

        public async void AcceptButton()
        {
            var helper = new AddNewProjectHelper();

            var validationResult = await helper.AddProjectToDatabase(this);

            if (validationResult.IsValid)
            {
                ApplicationWindows.ShowSuccesBox(validationResult.Message);
            }
            else
            {
                ApplicationWindows.ShowErrorBox(validationResult.Message);
            }
        }
    }
}
