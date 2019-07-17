namespace TaskManager.Tests.Infrastructure
{
    using Microsoft.AspNetCore.Identity;
    using Xunit;
    using TaskManager.Domain.Entity;
    using TaskManager.Persistence;

    public class ServicesFixture
    {
        public TaskManagerDbContext Context { get; private set; }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        public RoleManager<IdentityRole> RoleManager { get; private set; }

        public ServicesFixture()
        {
            var servicesModel = ServicesFactory.CreateProperServices();

            this.Context = servicesModel.Context;
            this.UserManager = servicesModel.UserManager;
            this.RoleManager = servicesModel.RoleManager;
        }

        [CollectionDefinition("ServicesTestCollection")]
        public class QueryCollection : ICollectionFixture<ServicesFixture>
        {
        }
    }
}
