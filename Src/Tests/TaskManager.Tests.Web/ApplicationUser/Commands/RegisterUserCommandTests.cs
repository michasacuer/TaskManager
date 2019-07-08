namespace TaskManager.Tests.Web
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Shouldly;
    using Xunit;
    using TaskManager.Application.Commands;
    using TaskManager.Persistence;

    public class RegisterUserCommandTests
    {

        public RegisterUserCommandTests()
        {
        }

        [Fact]
        public async Task RegisterUser()
        {
            var services = new ServiceCollection();

            services.AddDbContext<TaskManagerDbContext>(o => o.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            services.AddIdentity<Domain.Entity.ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<TaskManagerDbContext>();

            var provider = services.BuildServiceProvider();

            using (var scope = provider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Domain.Entity.ApplicationUser>>();

                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var signInManager = scope.ServiceProvider.GetRequiredService<SignInManager<Domain.Entity.ApplicationUser>>();

                var command = new RegisterCommand
                {
                    UserName = "test",
                    FirstName = "test",
                    LastName = "test",
                    Password = "test11",
                    Email = "test@wp.pl",
                    Role = Domain.Enum.Role.Manager
                };

                var commandHandler = new RegisterCommand.Handler(signInManager, userManager, roleManager);

                await commandHandler.Handle(command, CancellationToken.None);

                var user = await userManager.Users.FirstOrDefaultAsync(u => u.UserName == command.UserName);

                user.ShouldNotBeNull();
                user.UserName.ShouldBe(command.UserName);
            }
        }
    }
}
