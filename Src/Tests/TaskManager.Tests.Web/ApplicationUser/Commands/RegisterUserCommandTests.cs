namespace TaskManager.Tests.Web
{
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Shouldly;
    using Xunit;
    using TaskManager.Application.Commands;
    using TaskManager.Domain.Entity;
    using TaskManager.Persistence.Repository;
    using TaskManager.Tests.Infrastructure;
    using TaskManager.Infrastructure.Implementations;

    [Collection("ServicesTestCollection")]
    public class RegisterUserCommandTests
    {
        private readonly ApplicationUserService applicationUserService;

        private readonly UserManager<ApplicationUser> userManager;

        public RegisterUserCommandTests(ServicesFixture fixture)
        {
            var applicationUserRepository = new ApplicationUserRepository(
                fixture.UserManager,
                fixture.RoleManager,
                fixture.SignInManager,
                fixture.TokenService);

            this.userManager = fixture.UserManager;

            this.applicationUserService = new ApplicationUserService(applicationUserRepository);
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

            var commandHandler = new RegisterCommand.Handler(this.applicationUserService);

            await commandHandler.Handle(command, CancellationToken.None);

            var user = await userManager.Users.FirstOrDefaultAsync(u => u.UserName == command.UserName);

            user.ShouldNotBeNull();
            user.UserName.ShouldBe(command.UserName);
        }

        [Fact]
        public async Task RegisterShouldThrowExceptionWhenFormNotValid()
        {
            var command = new RegisterCommand
            {
                UserName = "test",
                FirstName = "test",
                LastName = "test",
                Password = "test11",
                Email = "?????",
                Role = Domain.Enum.Role.Manager
            };

            var commandHandler = new RegisterCommand.Handler(this.applicationUserService);

            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<ValidationException>();
        }
    }
}
