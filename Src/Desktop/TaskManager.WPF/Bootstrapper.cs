namespace TaskManager.WPF
{
    using System;
    using System.Net.Http;
    using System.Windows;
    using Caliburn.Micro;
    using TaskManager.Contracts.Data;
    using TaskManager.Contracts.Exceptions;
    using TaskManager.WPF.ViewModels;
    using TaskManager.WPF.ViewModels.Helper;

    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            this.Initialize();
        }

        protected override async void OnStartup(object sender, StartupEventArgs e)
        {
            var test = new TestConnections();

            try
            {
                await test.CheckServerConnection();
                await this.DisplayRootViewFor<MainWindowViewModel>();
            }
            catch (Exception exception)
            {
                if (exception is NotFoundServerException)
                {
                    ApplicationWindows.ShowErrorBox(exception.Message);
                }
                if (exception is HttpRequestException)
                {
                    ApplicationWindows.ShowErrorBox("Uruchom localhost (PowerShell > dotnet run)!");
                }
            }
        }
    }
}
