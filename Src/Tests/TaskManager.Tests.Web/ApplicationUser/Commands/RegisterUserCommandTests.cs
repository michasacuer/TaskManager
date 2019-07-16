namespace TaskManager.Tests.Web
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Shouldly;
    using Xunit;
    using TaskManager.Application.Commands;
    using TaskManager.Tests.Infrastructure;

    [Collection("DatabaseTestCollection")]
    public class RegisterUserCommandTests
    {
        private readonly UserManager<Domain.Entity.ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public RegisterUserCommandTests(DatabaseFixture fixture)
        {
            this.userManager = fixture.UserManager;
            this.roleManager = fixture.RoleManager;
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

            var commandHandler = new RegisterCommand.Handler(userManager, roleManager);

            await commandHandler.Handle(command, CancellationToken.None);

            var user = await userManager.Users.FirstOrDefaultAsync(u => u.UserName == command.UserName);

            user.ShouldNotBeNull();
            user.UserName.ShouldBe(command.UserName);
        }
    }
}
