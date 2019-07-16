namespace TaskManager.Tests.Web
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;
    using TaskManager.Application.Task.Commands.DeleteTask;
    using TaskManager.Common.Exceptions;
    using TaskManager.Persistence;
    using TaskManager.Tests.Infrastructure;

    [Collection("ServicesTestCollection")]
    public class DeleteTaskCommandTests
    {
        private readonly TaskManagerDbContext context;

        public DeleteTaskCommandTests(ServicesFixture fixture)
        {
            this.context = fixture.Context;
        }

        [Fact]
        public async Task EnsureThatTaskWasDeletedFromDatabase()
        {
            var task = await this.context.Tasks.FindAsync(1);

            var command = new DeleteTaskCommand { TaskId = task.Id };
            var commandHandler = new DeleteTaskCommand.Handler(this.context);

            await commandHandler.Handle(command, CancellationToken.None);

            var deletedTask = this.context.Tasks.FirstOrDefault(t => t.Id == 1);
            deletedTask.ShouldBe(null);
        }

        [Fact]
        public async Task DeleteTaskShouldThrowWhenProjectNotFound()
        {
            var command = new DeleteTaskCommand { TaskId = 2323223 };
            var commandHandler = new DeleteTaskCommand.Handler(this.context);

            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<EntityNotFoundException>();
        }
    }
}
