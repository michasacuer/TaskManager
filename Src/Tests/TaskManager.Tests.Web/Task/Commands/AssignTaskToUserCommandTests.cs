namespace TaskManager.Tests.Web
{
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Shouldly;
    using Xunit;
    using TaskManager.Application.Task.Commands.AssignTaskToUser;
    using TaskManager.Common.Exceptions;
    using TaskManager.Persistence;
    using TaskManager.Tests.Infrastructure;

    [Collection("ServicesTestCollection")]
    public class AssignTaskToUserCommandTests
    {
        private readonly TaskManagerDbContext context;

        private readonly UserManager<Domain.Entity.ApplicationUser> userManager;

        public AssignTaskToUserCommandTests(ServicesFixture fixture)
        {
            this.context = fixture.Context;
            this.userManager = fixture.UserManager;
        }

        [Fact]
        public async Task AssignTaskToUserCommandAssignUserIdToTask()
        {
            var user = await this.context.Users.FirstOrDefaultAsync(u => u.LastName == "Name2");

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
                TaskId = 4,
                ApplicationUserId = "dddddd"
            };

            var commandHandler = new AssignTaskToUserCommand.Handler(this.context, this.userManager);
            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<UserNotFoundException>();
        }

        [Fact]
        public async Task AssignTaskToUserShouldThrowExceptionWhenTaskIsAlredyAssignToAnotherUser()
        {
            var user = await this.context.Users.FirstOrDefaultAsync(u => u.LastName == "Name1");

            var command = new AssignTaskToUserCommand
            {
                TaskId = 3,
                ApplicationUserId = user.Id
            };

            var commandHandler = new AssignTaskToUserCommand.Handler(this.context, userManager);
            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task AssignTaskToUserShouldThrowExceptionWhenApplicationUserIdIsEmpty()
        {
            var command = new AssignTaskToUserCommand
            {
                TaskId = 3
            };

            var commandHandler = new AssignTaskToUserCommand.Handler(this.context, userManager);
            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task AssignTaskToUserShouldThrowExceptionWhenTaskNotFound()
        {
            var user = await this.context.Users.FirstOrDefaultAsync(u => u.LastName == "Name1");

            var command = new AssignTaskToUserCommand
            {
                TaskId = 33213123,
                ApplicationUserId = user.Id
            };

            var commandHandler = new AssignTaskToUserCommand.Handler(this.context, userManager);
            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<EntityNotFoundException>();
        }
    }
}
