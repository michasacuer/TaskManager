namespace TaskManager.WPF.ViewModels
{
    using System.Threading.Tasks;
    using System.Windows;
    using Caliburn.Micro;
    using TaskManager.WPF.Helpers;
    using TaskManager.WPF.Models;
    using TaskManager.WPF.ViewModels.Helper;

    public class ActiveTaskViewModel : Screen
    {
        private readonly ActiveTaskHelper helper;

        private readonly ActiveTask activeTask;

        private readonly MainWindowViewModel mainWindowViewModel;

        public ActiveTaskViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.helper = new ActiveTaskHelper();
            this.mainWindowViewModel = mainWindowViewModel;
            this.activeTask = LoggedUser.Instance.GetUserTask();
            
            this.ActiveTaskTextBlock = $"{this.activeTask.Task.Name}, Priorytet: {this.activeTask.Task.Priority}";
            this.DescriptionTextBlock = this.activeTask.Task.Description;
            this.TimerActiveTaskTextBlock = this.activeTask.Task.StartTime.ToString();
        }

        public string ActiveTaskTextBlock { get; set; }

        public string DescriptionTextBlock { get; set; }

        public string TimerActiveTaskTextBlock { get; set; }

        public async Task EndTaskButton()
        {
            this.mainWindowViewModel.IsActiveTaskButtonVisible = Visibility.Hidden;
            this.mainWindowViewModel.NotifyOfPropertyChange(() => this.mainWindowViewModel.IsActiveTaskButtonVisible);

            bool isSucceed = await this.helper.EndTaskByUser(this.activeTask.Task);
            if (isSucceed)
            {
                this.activeTask.Task = null;
                ApplicationWindows.ShowSuccesBox("Task zakończono pomyślnie!");
            }
            else
            {
                ApplicationWindows.ShowErrorBox("Błąd przy zakończeniu taska!");
            }

            this.TryCloseAsync();
        }

        public void CancelTaskButton() => this.TryCloseAsync();
    }
}
