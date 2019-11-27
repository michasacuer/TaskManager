namespace TaskManager.WPF.ViewModels
{
    using Caliburn.Micro;
    using TaskManager.Entity;
    using TaskManager.WPF.Helpers;
    using TaskManager.WPF.Models;
    using TaskManager.WPF.ViewModels.Helper;

    public class InfoTaskBoxViewModel : Screen
    {
        private InfoBoxHelper helper;

        private ToDoTask taskToEdit;

        public InfoTaskBoxViewModel(ToDoTask task)
        {
            this.helper = new InfoBoxHelper();

            this.taskToEdit = task;
            this.NavbarTaskName = task.Name;
            this.IdTextBox = task.Id.ToString();
            this.NameTextBox = task.Name;
            this.PriorityTextBox = task.Priority.ToString();
            this.StoryPointsTextBox = task.StoryPoints.ToString();
            this.ProjectIdTextBox = task.ProjectId.ToString();
            this.StartDateTextBox = task.StartTime.ToString();
            this.DescriptionTextBox = task.Description;
        }

        public string NavbarTaskName { get; set; }

        public string IdTextBox { get; set; }

        public string NameTextBox { get; set; }

        public string PriorityTextBox { get; set; }

        public string StoryPointsTextBox { get; set; }

        public string ProjectIdTextBox { get; set; }

        public string StartDateTextBox { get; set; }

        public string DescriptionTextBox { get; set; }

        public async void SaveButton()
        {
            if (LoggedUser.Instance.IsManager())
            {
                this.taskToEdit.Name = this.NameTextBox;
                this.taskToEdit.Description = this.DescriptionTextBox;

                bool isSucceed = await this.helper.EditTask(this.taskToEdit);
                if (isSucceed)
                {
                    ApplicationWindows.ShowSuccesBox("Pomyślnie edytowano zadanie!");
                }
                else
                {
                    ApplicationWindows.ShowErrorBox("Błąd podczas edycji taska!");
                }

                this.TryCloseAsync();
            }
            else
            {
                ApplicationWindows.ShowErrorBox("Nie masz uprawnień do edytowania zadań!");
            }
        }

        public void CancelButton() => this.TryCloseAsync();
    }
}
