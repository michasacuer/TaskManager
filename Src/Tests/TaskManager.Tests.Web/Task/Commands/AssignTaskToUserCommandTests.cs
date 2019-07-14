namespace TaskManager.Tests.Web.Task.Commands
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

    [Collection("DatabaseTestCollection")]
    public class AssignTaskToUserCommandTests
    {
        private readonly TaskManagerDbContext context;

        public AssignTaskToUserCommandTests(DatabaseFixture fixture)
        {
            this.context = fixture.Context;
        }

        [Fact]
        public async Task AssignTaskToUserCommandAssignUserIdToTask()
        {
            var user = this.context.Users.FirstOrDefault();

            var command = new AssignTaskToUserCommand
            {
                TaskId = 3,
                ApplicationUserId = user.Id
            };

            var commandHandler = new AssignTaskToUserCommand.Handler(this.context);
            await commandHandler.Handle(command, CancellationToken.None);

            var task = await this.context.Tasks.FindAsync(3);

            task.ApplicationUserId.ShouldBe(user.Id);
        }

        [Fact]
        public async Task AssignTaskToUserShouldThrowExceptionWhenApplicationUserNotFound()
        {
            var command = new AssignTaskToUserCommand
            {
                TaskId = 4
            };

            var commandHandler = new AssignTaskToUserCommand.Handler(this.context);

            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<UserNotFoundException>();
        }
    }
}
