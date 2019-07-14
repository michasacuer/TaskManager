namespace TaskManager.Tests.Web
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation;
    using Shouldly;
    using Xunit;
    using TaskManager.Application.Task.Commands.CreateTask;
    using TaskManager.Common.Exceptions;
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
                ProjectId = 3
            };

            var commandHandler = new CreateTaskCommand.Handler(this.context);
            await commandHandler.Handle(command, CancellationToken.None);

            var task = this.context.Tasks.FirstOrDefault(t => t.Name.Equals(command.Name));

            task.ShouldNotBeNull();
            task.Description.ShouldBe(command.Description);
        }

        [Fact]
        public async Task CreateTaskThrowExceptionWhenNameIsNull()
        {
            var command = new CreateTaskCommand
            {
                Description = "Description",
                Priority = Domain.Enum.Priority.Low,
                ProjectId = 3
            };

            var commandHandler = new CreateTaskCommand.Handler(this.context);
            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task CreateTaskWithoutDescriptionWillCreateTaskInDatabase()
        {
            var command = new CreateTaskCommand
            {
                Name = "Task2",
                Priority = Domain.Enum.Priority.Low,
                ProjectId = 3
            };

            var commandHandler = new CreateTaskCommand.Handler(this.context);
            await commandHandler.Handle(command, CancellationToken.None);

            var task = this.context.Tasks.FirstOrDefault(t => t.Name.Equals(command.Name));

            task.ShouldNotBeNull();
            task.Name.ShouldBe(command.Name);
        }

        [Fact]
        public async Task CreateTaskThrowExceptionWhenProjectNotFound()
        {
            var command = new CreateTaskCommand
            {
                Name = "Task3",
                Priority = Domain.Enum.Priority.Low,
                ProjectId = 1321424
            };

            var commandHandler = new CreateTaskCommand.Handler(this.context);
            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<EntityNotFoundException>();
        }
    }
}
