namespace TaskManager.Tests.Web
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Shouldly;
    using Xunit;
    using TaskManager.Application.Task.Commands.AssignTaskToUser;
    using TaskManager.Common.Exceptions;
    using TaskManager.Persistence;
    using TaskManager.Tests.Infrastructure;

    [Collection("DatabaseTestCollection")]
    public class AssignTaskToUserCommandTests
    {
        private readonly TaskManagerDbContext context;

        private readonly UserManager<Domain.Entity.ApplicationUser> userManager;

        private readonly RoleManager<IdentityRole> roleManager;

        public AssignTaskToUserCommandTests(DatabaseFixture fixture)
        {
            this.context = fixture.Context;
            this.userManager = fixture.UserManager;
            this.roleManager = fixture.RoleManager;
        }

        [Fact]
        public async Task AssignTaskToUserCommandAssignUserIdToTask()
        {
            this.context.Users.Add(new Domain.Entity.ApplicationUser
            {
            });

            this.context.SaveChanges();

            var user = this.context.Users.FirstOrDefault();
            await this.userManager.UpdateSecurityStampAsync(user);

            var role = new IdentityRole("Developer");
            await this.roleManager.CreateAsync(role);

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

        [Fact]
        public async Task AssignTaskToUserShouldThrowExceptionWhenApplicationUserNotFound()
        {
            var command = new AssignTaskToUserCommand
            {
                ApplicationUserId = "dddddd",
                TaskId = 4
            };

            var commandHandler = new AssignTaskToUserCommand.Handler(this.context, this.userManager);
            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<UserNotFoundException>();
        }
    }
}
