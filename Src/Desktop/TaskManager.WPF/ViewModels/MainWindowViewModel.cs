namespace TaskManager.WPF.ViewModels
{
    using System.Windows;
    using Caliburn.Micro;

    public class MainWindowViewModel : Conductor<IScreen>.Collection.OneActive
    {
        public Visibility IsActiveTaskButtonVisible { get; set; } = Visibility.Hidden;
    }
}
