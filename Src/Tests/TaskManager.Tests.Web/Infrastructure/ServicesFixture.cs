namespace TaskManager.Tests.Infrastructure
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
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
            var services = ServicesFactory.Create().BuildServiceProvider().CreateScope();

            this.Context = services.ServiceProvider.GetRequiredService<TaskManagerDbContext>();
            this.UserManager = services.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            this.RoleManager = services.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            ContextDataSeeding.Run(this.Context, this.RoleManager, this.UserManager);
            ContextDataSeeding.AddRolesToUsers(this.Context, this.RoleManager, this.UserManager);
        }

        [CollectionDefinition("ServicesTestCollection")]
        public class QueryCollection : ICollectionFixture<ServicesFixture>
        {
        }
    }
}
