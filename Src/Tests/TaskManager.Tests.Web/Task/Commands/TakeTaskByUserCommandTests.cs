namespace TaskManager.Tests.Web
{
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation;
    using Microsoft.EntityFrameworkCore;
    using Shouldly;
    using Xunit;
    using TaskManager.Application.Task.Commands.TakeTaskByUser;
    using TaskManager.Application.Interfaces;
    using TaskManager.Common.Exceptions;
    using TaskManager.Domain.Entity;
    using TaskManager.Persistence.Repository;
    using TaskManager.Persistence;
    using TaskManager.Tests.Infrastructure;

    [Collection("ServicesTestCollection")]
    public class TakeTaskByUserCommandTests
    {
        private readonly ApplicationUserRepository applicationUserRepository;

        private readonly IRepository<ToDoTask> taskRepository;

        private readonly TaskManagerDbContext context;

        private readonly INotificationService notificationService;

        public TakeTaskByUserCommandTests(ServicesFixture fixture)
        {
            this.applicationUserRepository = new ApplicationUserRepository(
                fixture.UserManager,
                fixture.RoleManager,
                fixture.SignInManager,
                fixture.TokenService);

            this.taskRepository = new Repository<ToDoTask>(fixture.Context);
            this.context = fixture.Context;
            this.notificationService = fixture.NotificationService;
        }

        [Fact]
        public async Task TakeTaskByUserCommandAssignUserIdToTask()
        {
            var user = await this.context.Users.FirstOrDefaultAsync(u => u.LastName == "Name2");

            var command = new TakeTaskByUserCommand
            {
                TaskId = 4,
                ApplicationUserId = user.Id
            };

            var commandHandler = new TakeTaskByUserCommand.Handler(
                this.applicationUserRepository, 
                this.taskRepository, 
                this.notificationService);

            await commandHandler.Handle(command, CancellationToken.None);

            var task = await this.context.Tasks.FindAsync(4);
            task.ApplicationUserId.ShouldBe(user.Id);
        }

        [Fact]
        public async Task TakeTaskByUserShouldThrowExceptionWhenApplicationUserNotFound()
        {
            var command = new TakeTaskByUserCommand
            {
                TaskId = 4,
                ApplicationUserId = "dddddd"
            };

            var commandHandler = new TakeTaskByUserCommand.Handler(
                this.applicationUserRepository,
                this.taskRepository,
                this.notificationService);

            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<UserNotFoundException>();
        }

        [Fact]
        public async Task TakeTaskByUserShouldThrowExceptionWhenTaskIsAlredyAssignToAnotherUser()
        {
            var user = await this.context.Users.FirstOrDefaultAsync(u => u.LastName == "Name1");

            var command = new TakeTaskByUserCommand
            {
                TaskId = 5,
                ApplicationUserId = user.Id
            };

            var commandHandler = new TakeTaskByUserCommand.Handler(
                this.applicationUserRepository,
                this.taskRepository,
                this.notificationService);

            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task TakeTaskByUserShouldThrowExceptionWhenApplicationUserIdIsEmpty()
        {
            var command = new TakeTaskByUserCommand
            {
                TaskId = 4
            };

            var commandHandler = new TakeTaskByUserCommand.Handler(
                this.applicationUserRepository, 
                this.taskRepository,
                this.notificationService);

            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task TakeTaskByUserShouldThrowExceptionWhenTaskNotFound()
        {
            var user = await this.context.Users.FirstOrDefaultAsync(u => u.LastName == "Name1");

            var command = new TakeTaskByUserCommand
            {
                TaskId = 33213123,
                ApplicationUserId = user.Id
            };

            var commandHandler = new TakeTaskByUserCommand.Handler(
                this.applicationUserRepository,
                this.taskRepository,
                this.notificationService);

            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<EntityNotFoundException>();
        }
    }
}
