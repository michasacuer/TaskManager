namespace TaskManager.Tests.Infrastructure
{
    using Microsoft.AspNetCore.Identity;
    using Moq;
    using Xunit;
    using TaskManager.Application.Interfaces;
    using TaskManager.Domain.Entity;
    using TaskManager.Infrastructure.Implementations;
    using TaskManager.Persistence;

    public class ServicesFixture
    {
        public TaskManagerDbContext Context { get; private set; }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        public RoleManager<IdentityRole> RoleManager { get; private set; }

        public SignInManager<ApplicationUser> SignInManager { get; private set; }

        public ITokenService TokenService { get; private set; }

        public INotificationService NotificationService { get; private set; }

        public ServicesFixture()
        {
            var servicesModel = ServicesFactory.CreateProperServices();

            this.Context = servicesModel.Context;
            this.UserManager = servicesModel.UserManager;
            this.RoleManager = servicesModel.RoleManager;
            this.SignInManager = servicesModel.SignInManager;
            this.TokenService = servicesModel.TokenService;

            var service = new Mock<INotificationService>();
            service.Setup(s => s.SendMessageToAll(It.IsAny<string>()));
            this.NotificationService = service.Object;
        }

        [CollectionDefinition("ServicesTestCollection")]
        public class QueryCollection : ICollectionFixture<ServicesFixture>
        {
        }
    }
}
