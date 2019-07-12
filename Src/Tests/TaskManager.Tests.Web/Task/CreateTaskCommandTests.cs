namespace TaskManager.Tests.Web
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;
    using TaskManager.Application.Task.Commands;
    using TaskManager.Persistence;
    using TaskManager.Tests.Infrastructure;

    [Collection("DatabaseTestCollection")]
    public class CreateTaskCommandTests
    {
        private readonly TaskManagerDbContext context;

        public CreateTaskCommandTests(DatabaseFixture fixture)
        {
            this.context = fixture.Context;
        }

        [Fact]
        public async Task CreateTaskCommandCreateTaskInDatabase()
        {
            var command = new CreateTaskCommand
            {
                Name = "Task",
                Description = "Description",
                Priority = Domain.Enum.Priority.Low,
                ProjectId = 1
            };

            var commandHandler = new CreateTaskCommand.Handler(this.context);
            await commandHandler.Handle(command, CancellationToken.None);

            var task = this.context.Tasks.FirstOrDefault(t => t.Name.Equals(command.Name));

            task.ShouldNotBeNull();
            task.Description.ShouldBe(command.Description);
        }
    }
}
