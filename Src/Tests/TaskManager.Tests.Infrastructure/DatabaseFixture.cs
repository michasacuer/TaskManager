namespace TaskManager.Tests.Infrastructure
{
    using System;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Xunit;
    using TaskManager.Domain.Entity;
    using TaskManager.Persistence;

    public class DatabaseFixture
    {
        public TaskManagerDbContext Context { get; private set; }

        public Mock<UserManager<ApplicationUser>> MockUserManager { get; private set; }

        public Mock<RoleManager<ApplicationUser>> MockRoleManager { get; private set; }

        public Mock<SignInManager<ApplicationUser>> MockSignInManager { get; private set; }

        public DatabaseFixture()
        {
            this.Context = DatabaseContextFactory.Create();

            this.MockUserManager = new Mock<UserManager<ApplicationUser>>(
                new Mock<IUserStore<ApplicationUser>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<ApplicationUser>>().Object,
                new IUserValidator<ApplicationUser>[0],
                new IPasswordValidator<ApplicationUser>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<ApplicationUser>>>().Object);

            this.MockRoleManager = new Mock<RoleManager<ApplicationUser>>(
                new Mock<IRoleStore<IdentityRole>>().Object,
                new IRoleValidator<IdentityRole>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<ILogger<RoleManager<IdentityRole>>>().Object);

            this.MockSignInManager = new Mock<SignInManager<ApplicationUser>>(
                 new Mock<IHttpContextAccessor>().Object,
                 new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>().Object,
                 new Mock<IOptions<IdentityOptions>>().Object,
                 new Mock<ILogger<SignInManager<ApplicationUser>>>().Object,
                 new Mock<IAuthenticationSchemeProvider>().Object);
        }
    }

    [CollectionDefinition("DatabaseTestCollection")]
    public class QueryCollection : ICollectionFixture<DatabaseFixture>
    {
    }
}
