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

            var services = ServicesFactory.Create().BuildServiceProvider().CreateScope();

            this.UserManager = services.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            this.RoleManager = services.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            DataSeeding.AddRolesToUsers(this.Context, this.RoleManager, this.UserManager);
        }

        [CollectionDefinition("DatabaseTestCollection")]
        public class QueryCollection : ICollectionFixture<DatabaseFixture>
        {
        }
    }
}
