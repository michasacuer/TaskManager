namespace TaskManager.Tests.Infrastructure
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;
    using TaskManager.Domain.Entity;
    using TaskManager.Persistence;

    public class DatabaseFixture
    {
        public TaskManagerDbContext Context { get; private set; }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        public RoleManager<IdentityRole> RoleManager { get; private set; }

        public DatabaseFixture()
        {
            this.Context = DatabaseContextFactory.Create();

            var scope = ServicesFactory.Create().BuildServiceProvider().CreateScope();

            this.UserManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            this.RoleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        }

        [CollectionDefinition("DatabaseTestCollection")]
        public class QueryCollection : ICollectionFixture<DatabaseFixture>
        {
        }
    }
}
