namespace TaskManager.Tests.Infrastructure
{
    using Microsoft.AspNetCore.Identity;
    using Xunit;
    using TaskManager.Domain.Entity;
    using TaskManager.Persistence;
    using TaskManager.Application.Interfaces;

    public class ServicesFixture
    {
        public TaskManagerDbContext Context { get; private set; }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        public RoleManager<IdentityRole> RoleManager { get; private set; }

        public SignInManager<ApplicationUser> SignInManager { get; private set; }

        public ITokenService TokenService { get; private set; }

        public ServicesFixture()
        {
            var servicesModel = ServicesFactory.CreateProperServices();

            this.Context = servicesModel.Context;
            this.UserManager = servicesModel.UserManager;
            this.RoleManager = servicesModel.RoleManager;
            this.SignInManager = servicesModel.SignInManager;
            this.TokenService = servicesModel.TokenService;
        }

        [CollectionDefinition("ServicesTestCollection")]
        public class QueryCollection : ICollectionFixture<ServicesFixture>
        {
        }
    }
}
