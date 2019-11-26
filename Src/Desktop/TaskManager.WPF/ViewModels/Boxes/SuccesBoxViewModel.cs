namespace TaskManager.WPF.ViewModels
{
    using Caliburn.Micro;

    public class SuccesBoxViewModel : Screen
    {
        public SuccesBoxViewModel(string alert)
        {
            this.SuccesTextBox = alert;
        }

        public string SuccesTextBox { get; set; }

        public void OkButton() => this.TryCloseAsync();
    }
}
