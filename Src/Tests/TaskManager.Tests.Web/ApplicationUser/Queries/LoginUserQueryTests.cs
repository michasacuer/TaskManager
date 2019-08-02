namespace TaskManager.Tests.Web
{
    using Microsoft.AspNetCore.Identity;
    using Xunit;
    using TaskManager.Domain.Entity;
    using TaskManager.Persistence.Repository;
    using TaskManager.Tests.Infrastructure;

    [Collection("ServicesTestCollection")]
    public class LoginUserQueryTests
    {
        private readonly ApplicationUserRepository applicationUserRepository;

        private readonly UserManager<ApplicationUser> userManager;

        public LoginUserQueryTests(ServicesFixture fixture)
        {
            this.applicationUserRepository = new ApplicationUserRepository(
                fixture.UserManager,
                fixture.RoleManager,
                fixture.SignInManager,
                fixture.TokenService);

            this.userManager = fixture.UserManager;
        }
    }
}
