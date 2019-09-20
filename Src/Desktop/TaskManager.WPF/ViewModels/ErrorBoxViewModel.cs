namespace TaskManager.WPF.ViewModels
{
    using Caliburn.Micro;

    public class ErrorBoxViewModel : Screen
    {
        public ErrorBoxViewModel(string alert)
        {
            this.ErrorTextBox = alert;
        }

        public string ErrorTextBox { get; set; }

        public void OkButton() => this.TryCloseAsync();
    }
}

