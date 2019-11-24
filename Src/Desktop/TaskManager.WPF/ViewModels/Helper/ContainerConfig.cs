namespace TaskManager.WPF.ViewModels.Helper
{
    using Autofac;
    using TaskManager.Contracts.Account;
    using TaskManager.Contracts.Data;
    using TaskManager.Contracts.Interfaces;

    public class ContainerConfig
    {
        public static IContainer ConfigureContracts()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ProjectContract>().As<IProjectContract>();
            builder.RegisterType<TaskContract>().As<ITaskContract>();
            builder.RegisterType<EndedTaskContract>().As<IEndedTaskContract>();
            builder.RegisterType<NotificationContract>().As<INotificationContract>();
            builder.RegisterType<AccountContract>().As<IAccountContract>();

            return builder.Build();
        }
    }
}
