namespace TaskManager.Tests.Infrastructure
{
    using Microsoft.AspNetCore.Identity;
    using Moq;
    using Xunit;
    using TaskManager.Application.Interfaces;
    using TaskManager.Domain.Entity;
    using TaskManager.Persistence;

    public class ServicesMoqFixture
    {
        public Mock<TaskManagerDbContext> Context { get; private set; }

        public Mock<RoleManager<IdentityRole>> RoleManager { get; private set; }

        public Mock<SignInManager<ApplicationUser>> SignInManager { get; private set; }

        public Mock<ITokenService> TokenService { get; private set; }

        public Mock<INotificationService> NotificationService { get; private set; }

        public ServicesMoqFixture()
        {
            var servicesModel = ServicesFactory.CreateProperServices();

            this.Context = new Mock<TaskManagerDbContext>();
            this.RoleManager = new Mock<RoleManager<IdentityRole>>();
            this.SignInManager = new Mock<SignInManager<ApplicationUser>>();
            this.TokenService = new Mock<ITokenService>();

            var service = new Mock<INotificationService>();
            service.Setup(s => s.SendMessageToAll(It.IsAny<string>()));
            
            this.NotificationService = new Mock<INotificationService>();
        }

        [CollectionDefinition("ServicesMoqTestCollection")]
        public class QueryCollection : ICollectionFixture<ServicesMoqFixture>
        {
        }
    }
}