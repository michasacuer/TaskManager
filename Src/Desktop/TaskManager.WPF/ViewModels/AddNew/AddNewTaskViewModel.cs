namespace TaskManager.WPF.ViewModels
{
    using System.Collections.Generic;
    using Caliburn.Micro;
    using TaskManager.Entity;
    using TaskManager.Entity.Enum;
    using TaskManager.WPF.Helpers;
    using TaskManager.WPF.ViewModels.Helper;

    public class AddNewTaskViewModel : Screen
    {
        private AddNewTaskHelper helper;

        public Project SelectedProject { get; set; }

        public string DescriptionTextBox { get; set; }

        public string TaskNameTextBox { get; set; }

        public bool LowPriorityButton { get; set; }

        public bool MediumPriorityButton { get; set; }

        public bool HighPriorityButton { get; set; }

        public Priority Priority { get; set; }

        public List<Project> Projects { get; set; } 

        public async void AcceptButton()
        {
            var validationResult = await this.helper.AddTaskToDatabase(this);
            if (validationResult.IsValid)
            {
                ApplicationWindows.ShowSuccesBox(validationResult.Message);
            }
            else
            {
                ApplicationWindows.ShowErrorBox(validationResult.Message);
            }
        }

        protected async override void OnViewLoaded(object view)
        {
            this.helper = new AddNewTaskHelper();
            this.Projects = await this.helper.GetAllProjectsFromDatabase();
            this.NotifyOfPropertyChange(() => this.Projects);
        }
    }
}