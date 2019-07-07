namespace TaskManager.Tests.Web
{
    using Microsoft.AspNetCore.Identity;
    using Moq;
    using TaskManager.Tests.Infrastructure;
    using Xunit;

    [Collection("DatabaseTestCollection")]
    public class RegisterUserCommandTests
    {
        private readonly Mock<UserManager<Domain.Entity.ApplicationUser>> mockUserManager;

        private readonly Mock<RoleManager<Domain.Entity.ApplicationUser>> mockRoleManager;

        private readonly Mock<SignInManager<Domain.Entity.ApplicationUser>> mockSignInManager;

        public RegisterUserCommandTests(DatabaseFixture fixture)
        {
            mockUserManager = fixture.MockUserManager;
            mockRoleManager = fixture.MockRoleManager;
            mockSignInManager = fixture.MockSignInManager;
        }
    }
}
