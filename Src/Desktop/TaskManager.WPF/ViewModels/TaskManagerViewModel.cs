namespace TaskManager.WPF.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using Caliburn.Micro;
    using TaskManager.Entity;
    using TaskManager.WPF.Helpers;
    using TaskManager.WPF.Models;
    using TaskManager.WPF.ViewModels.Helper;

    public class TaskManagerViewModel : Screen
    {
        private TaskManagerHelper helper;

        private MainWindowViewModel mainWindowViewModel;

        private Project selectedProject;

        public TaskManagerViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.helper = new TaskManagerHelper();
            this.mainWindowViewModel = mainWindowViewModel;

            this.IsAcceptButtonEnabled = !LoggedUser.Instance.GetUserTask().IsTaskTakenByUser();
            this.NotifyOfPropertyChange(() => this.IsAcceptButtonEnabled);
        }

        public bool IsAcceptButtonEnabled { get; set; }
        
        public List<Project> Projects { get; set; }

        public List<ToDoTask> Tasks { get; set; }

        public ToDoTask SelectedTask { get; set; }

        public Project SelectedProject
        {
            get => this.selectedProject;
            set
            {
                this.selectedProject = value;

                try
                {
                    this.Tasks = value.Tasks;
                    this.NotifyOfPropertyChange(() => this.Tasks);
                }
                catch (ArgumentNullException)
                {
                    ApplicationWindows.ShowErrorBox($"Projekt o nazwie {this.selectedProject} nie ma tasków!");
                }
            }
        }

        public async void AcceptButton()
        {
            if (!LoggedUser.Instance.HavePermissionToTakeTask())
            {
                ApplicationWindows.ShowErrorBox("Brak uprawnień! Zgłoś się do administratora.");
                return;
            }

            if (this.SelectedTask == null)
            {
                ApplicationWindows.ShowErrorBox("Wybierz zadanie!");
                return;
            }

            var taskToActivate = await this.helper.GetTaskToActivate(this.SelectedTask);

            if (taskToActivate != null)
            {
                LoggedUser.Instance.AttachTaskToUser(taskToActivate);

                await this.TryCloseAsync();

                this.mainWindowViewModel.IsActiveTaskButtonVisible = Visibility.Visible;
                this.mainWindowViewModel.NotifyOfPropertyChange(() => this.mainWindowViewModel.IsActiveTaskButtonVisible);
            }
            else
            {
                await this.TryCloseAsync();
            }
        }

        public void CancelButton() => this.TryCloseAsync();

        protected async override void OnViewLoaded(object view)
        {
            this.Projects = await this.helper.GetAllProjectsFromDatabase();
            this.NotifyOfPropertyChange(() => this.Projects);
        }
    }
}