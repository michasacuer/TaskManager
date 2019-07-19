namespace TaskManager.Tests.Web
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Shouldly;
    using Xunit;
    using TaskManager.Application.Commands;
    using TaskManager.Domain.Entity;
    using TaskManager.Persistence.Repository;
    using TaskManager.Tests.Infrastructure;

    [Collection("ServicesTestCollection")]
    public class RegisterUserCommandTests
    {
        private readonly ApplicationUserRepository applicationUserRepository;

        private readonly UserManager<ApplicationUser> userManager;

        public RegisterUserCommandTests(ServicesFixture fixture)
        {
            this.applicationUserRepository = new ApplicationUserRepository(
                fixture.UserManager,
                fixture.RoleManager,
                fixture.SignInManager,
                fixture.TokenService);

            this.userManager = fixture.UserManager;
        }

        [Fact]
        public async Task RegisterUser()
        {
            var command = new RegisterCommand
            {
                UserName = "test",
                FirstName = "test",
                LastName = "test",
                Password = "test11",
                Email = "test@wp.pl",
                Role = Domain.Enum.Role.Manager
            };

            var commandHandler = new RegisterCommand.Handler(this.applicationUserRepository);

            await commandHandler.Handle(command, CancellationToken.None);

            var user = await userManager.Users.FirstOrDefaultAsync(u => u.UserName == command.UserName);

            user.ShouldNotBeNull();
            user.UserName.ShouldBe(command.UserName);
        }
    }
}
