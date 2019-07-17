namespace TaskManager.Tests.Web
{
    using System.Threading;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;
    using Microsoft.EntityFrameworkCore;
    using TaskManager.Application.Task.Commands.EndTaskByUser;
    using TaskManager.Persistence;

    [Collection("ServicesTestCollection")]
    public class EndTaskByUserCommandTests
    {
        private readonly TaskManagerDbContext context;

        public EndTaskByUserCommandTests(TaskManagerDbContext context)
        {
            this.context = context;
        }

        [Fact]
        public async Task EndTaskByUserCommandShouldMoveTaskToEndedTaskTable()
        {
            var user = await this.context.Users.FirstOrDefaultAsync();
            var task = await this.context.Tasks.FindAsync(3);

            string taskName = task.Name;

            var command = new EndTaskByUserCommand()
            {
                TaskId = task.Id,
                ApplicationUserId = user.Id
            };

            var commandHandler = new EndTaskByUserCommand.Handler(this.context);

            await commandHandler.Handle(command, CancellationToken.None);

            var endedTask = await this.context.EndedTasks.FirstOrDefaultAsync(et => et.Name == taskName);

            task.ShouldBeNull();
            endedTask.Name.ShouldBe(taskName);
        }
    }
}
