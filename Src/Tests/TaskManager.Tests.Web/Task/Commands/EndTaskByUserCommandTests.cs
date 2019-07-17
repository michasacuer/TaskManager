namespace TaskManager.Tests.Web
{
    using System.Threading;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;
    using Microsoft.EntityFrameworkCore;
    using TaskManager.Application.Task.Commands.EndTaskByUser;
    using TaskManager.Persistence;
    using TaskManager.Tests.Infrastructure;

    [Collection("ServicesTestCollection")]
    public class EndTaskByUserCommandTests
    {
        private readonly TaskManagerDbContext context;

        public EndTaskByUserCommandTests(ServicesFixture fixture)
        {
            this.context = fixture.Context;
        }

        [Fact]
        public async Task EndTaskByUserCommandShouldMoveTaskToEndedTaskTable()
        {
            var task = await this.context.Tasks.FindAsync(3);

            string taskName = task.Name;

            var command = new EndTaskByUserCommand()
            {
                TaskId = task.Id,
                ApplicationUserId = "NotEmpty"
            };

            var commandHandler = new EndTaskByUserCommand.Handler(this.context);

            await commandHandler.Handle(command, CancellationToken.None);

            var endedTask = await this.context.EndedTasks.FirstOrDefaultAsync(et => et.Name == taskName);
            var deletedTask = await this.context.Tasks.FindAsync(3);

            deletedTask.ShouldBeNull();
            endedTask.Name.ShouldBe(taskName);
        }
    }
}
