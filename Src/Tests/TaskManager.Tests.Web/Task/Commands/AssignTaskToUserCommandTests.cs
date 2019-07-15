namespace TaskManager.Tests.Web
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;
    using TaskManager.Application.Task.Commands.AssignTaskToUser;
    using TaskManager.Common.Exceptions;
    using TaskManager.Persistence;
    using TaskManager.Tests.Infrastructure;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Identity;

    [Collection("DatabaseTestCollection")]
    public class AssignTaskToUserCommandTests
    {
        private readonly TaskManagerDbContext context;

        private ServiceCollection Services { get; set; }

        public AssignTaskToUserCommandTests(DatabaseFixture fixture)
        {
            this.context = fixture.Context;
            this.Services = ServicesFactory.Create();
        }

        [Fact]
        public async Task AssignTaskToUserCommandAssignUserIdToTask()
        {
            this.context.Users.Add(new Domain.Entity.ApplicationUser
            {
            });

            this.context.SaveChanges();

            var provider = this.Services.BuildServiceProvider();
            using (var scope = provider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Domain.Entity.ApplicationUser>>();
                var user = this.context.Users.FirstOrDefault();
                await userManager.UpdateSecurityStampAsync(user);

                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var role = new IdentityRole("Developer");
                await roleManager.CreateAsync(role);

                await userManager.AddToRoleAsync(user, "Developer");

                var command = new AssignTaskToUserCommand
                {
                    TaskId = 3,
                    ApplicationUserId = user.Id
                };

                var commandHandler = new AssignTaskToUserCommand.Handler(this.context, userManager);
                await commandHandler.Handle(command, CancellationToken.None);

                var task = await this.context.Tasks.FindAsync(3);
                task.ApplicationUserId.ShouldBe(user.Id);
            }
        }

        [Fact]
        public async Task AssignTaskToUserShouldThrowExceptionWhenApplicationUserNotFound()
        {
            var command = new AssignTaskToUserCommand
            {
                ApplicationUserId = "dddddd",
                TaskId = 4
            };

            var provider = this.Services.BuildServiceProvider();
            using (var scope = provider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Domain.Entity.ApplicationUser>>();

                var commandHandler = new AssignTaskToUserCommand.Handler(this.context, userManager);
                await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<UserNotFoundException>();
            }

        
        }
    }
}
