namespace TaskManager.WPF.ViewModels
{
    using Caliburn.Micro;
    using TaskManager.Entity;
    using TaskManager.WPF.Helpers;
    using TaskManager.WPF.ViewModels.Helper;

    public class DeleteTaskBoxViewModel : Screen
    {
        private DeleteFromDatabaseHelper helper;

        private ToDoTask taskToDelete;

        public DeleteTaskBoxViewModel(ToDoTask task)
        {
            this.helper = new DeleteFromDatabaseHelper();

            this.taskToDelete = task;
            this.DeleteName = task.Name;
        }

        public string DeleteName { get; set; }

        public async void YesButton()
        {
            bool isSucceed = await helper.DeleteTask(this.taskToDelete.Id);
            if (isSucceed)
            {
                ApplicationWindows.ShowSuccesBox($"Task {this.DeleteName} usunięto pomyślnie!");
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
