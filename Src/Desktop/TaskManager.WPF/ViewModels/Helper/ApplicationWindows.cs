namespace TaskManager.WPF.ViewModels.Helper
{
    using System;
    using System.Diagnostics;
    using Caliburn.Micro;
    using TaskManager.Entity;
    using TaskManager.WPF.ViewModels;

    public static class ApplicationWindows
    {
        private static IWindowManager manager = new WindowManager();

        public static void ShowErrorBox(string alert) => manager.ShowDialogAsync(new ErrorBoxViewModel(alert), null, null);

        public static void ShowSuccesBox(string alert) => manager.ShowDialogAsync(new SuccesBoxViewModel(alert), null, null);

        public static void ShowLoginBox(MainWindowViewModel mainWindowViewModel) 
            => manager.ShowDialogAsync(new LoginViewModel(mainWindowViewModel), null, null);

        public static void ShowRegistrationBox() => manager.ShowDialogAsync(new RegistrationViewModel(), null, null);

        public static void ShowManagerPanelBox() => manager.ShowDialogAsync(new ManagerPanelViewModel(), null, null);

        public static void ShowPdf(string filepath)
        {
            var process = new Process();
            var pdf = new Uri(filepath, UriKind.RelativeOrAbsolute);

            process.StartInfo.FileName = pdf.LocalPath;
            process.Start();
        }

        public static void ShowInfoProjectBox(Project project) => manager.ShowDialogAsync(new InfoProjectBoxViewModel(project), null, null);

        public static void ShowDeleteProjectBox(Project project) => manager.ShowDialogAsync(new DeleteProjectBoxViewModel(project), null, null);

        public static void ShowInfoTaskBox(ToDoTask task) => manager.ShowDialogAsync(new InfoTaskBoxViewModel(task), null, null);

        public static void ShowDeleteTaskBox(ToDoTask task) => manager.ShowDialogAsync(new DeleteTaskBoxViewModel(task), null, null);

        public static void ShowActiveTask(MainWindowViewModel mainWindowViewModel)
            => manager.ShowDialogAsync(new ActiveTaskViewModel(mainWindowViewModel), null, null);
    }
}
