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
    using TaskManager.Persistence.Repository;
    using TaskManager.Domain.Entity;
    using TaskManager.Application.Interfaces;

    [Collection("ServicesTestCollection")]
    public class EndTaskByUserCommandTests
    {
        private readonly TaskManagerDbContext context;

        private readonly IRepository<ToDoTask> taskRepository;

        private readonly INotificationService notificationService;

        public EndTaskByUserCommandTests(ServicesFixture fixture)
        {
            this.context = fixture.Context;
            this.taskRepository = new Repository<ToDoTask>(this.context);
            this.notificationService = fixture.NotificationService;
        }

        [Fact]
        public async Task EndTaskByUserCommandShouldMoveTaskToEndedTaskTable()
        {
            var task = await this.context.Tasks.FindAsync(3);

            var command = new EndTaskByUserCommand()
            {
                TaskId = task.Id,
                ApplicationUserId = "NotEmpty"
            };

            var commandHandler = new EndTaskByUserCommand.Handler(
                this.taskRepository,
                this.notificationService);

            await commandHandler.Handle(command, CancellationToken.None);

            var deletedTask = await this.context.Tasks.FindAsync(3);

            deletedTask.IsDeleted.ShouldBeTrue();
        }
    }
}
