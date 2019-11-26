namespace TaskManager.Tests.Web
{
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation;
    using Shouldly;
    using Xunit;
    using TaskManager.Persistence;
    using TaskManager.Persistence.Repository;
    using TaskManager.Tests.Infrastructure;
    using TaskManager.Application.Commands.EditProject;
    using TaskManager.Domain.Entity;
    using TaskManager.Common.Exceptions;
    using TaskManager.Application.Task.Commands.EditTask;

    [Collection("ServicesTestCollection")]
    public class EditTaskCommandTests
    {
        private readonly TaskManagerDbContext context;

        private readonly TaskRepository taskRepository;

        public EditTaskCommandTests(ServicesFixture fixture)
        {
            this.context = fixture.Context;
            this.taskRepository = new TaskRepository(fixture.Context);
        }

        [Fact]
        public async Task EditTaskShouldEditProjectInDatabase()
        {
            var taskBefore = await this.context.Tasks.FindAsync(2);
            string nameBefore = taskBefore.Name;

            var command = new EditTaskCommand
            {
                Data = new ToDoTask
                {
                    Id = 2,
                    Name = "ChangedName!",
                    Description = "ChangedDesc!"
                }
            };

            var commandHandler = new EditTaskCommand.Handler(this.taskRepository);

            await commandHandler.Handle(command, CancellationToken.None);

            var task = await this.context.Tasks.FindAsync(2);

            task.ShouldNotBeNull();
            task.Name.ShouldBe(command.Data.Name);
            nameBefore.ShouldNotBe(task.Name);
        }

        [Fact]
        public async Task EditTaskShouldThrowWhenProjectNotFound()
        {
            var command = new EditTaskCommand
            {
                Data = new ToDoTask
                {
                    Id = 1111111111,
                    Name = "ChangedName!",
                    Description = "ChangedDesc!"
                }
            };

            var commandHandler = new EditTaskCommand.Handler(this.taskRepository);

            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<EntityNotFoundException>();
        }

        [Fact]
        public async Task EditTaskShouldThrowWhenProjectNameIsEmpty()
        {
            var command = new EditTaskCommand
            {
                Data = new ToDoTask
                {
                    Id = 1,
                    Name = "",
                    Description = "ChangedDesc!"
                }
            };

            var commandHandler = new EditTaskCommand.Handler(this.taskRepository);

            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task EditTaskShouldEditWhenDescOnlyEdited()
        {
            var taskBefore = await this.context.Tasks.FindAsync(7);
            string nameBefore = taskBefore.Name;
            string descriptionBefore = taskBefore.Description;

            var command = new EditTaskCommand
            {
                Data = new ToDoTask
                {
                    Id = 7,
                    Name = nameBefore,
                    Description = "ChangedDescAAAAAAA!"
                }
            };

            var commandHandler = new EditTaskCommand.Handler(this.taskRepository);

            await commandHandler.Handle(command, CancellationToken.None);

            var task = await this.context.Tasks.FindAsync(7);

            task.ShouldNotBeNull();
            task.Name.ShouldBe(command.Data.Name);
            nameBefore.ShouldBe(task.Name);
            descriptionBefore.ShouldNotBe(task.Description);
        }
    }
}
