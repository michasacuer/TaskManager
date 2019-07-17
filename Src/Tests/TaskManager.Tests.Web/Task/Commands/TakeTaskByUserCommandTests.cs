namespace TaskManager.Tests.Web
{
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Shouldly;
    using Xunit;
    using TaskManager.Common.Exceptions;
    using TaskManager.Persistence;
    using TaskManager.Tests.Infrastructure;
    using TaskManager.Application.Task.Commands.TakeTaskByUser;

    [Collection("ServicesTestCollection")]
    public class TakeTaskByUserCommandTests
    {
        private readonly TaskManagerDbContext context;

        private readonly UserManager<Domain.Entity.ApplicationUser> userManager;

        public TakeTaskByUserCommandTests(ServicesFixture fixture)
        {
            this.context = fixture.Context;
            this.userManager = fixture.UserManager;
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

            var commandHandler = new TakeTaskByUserCommand.Handler(this.context, userManager);
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

            var commandHandler = new TakeTaskByUserCommand.Handler(this.context, this.userManager);
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

            var commandHandler = new TakeTaskByUserCommand.Handler(this.context, userManager);
            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task TakeTaskByUserShouldThrowExceptionWhenApplicationUserIdIsEmpty()
        {
            var command = new TakeTaskByUserCommand
            {
                TaskId = 4
            };

            var commandHandler = new TakeTaskByUserCommand.Handler(this.context, userManager);
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

            var commandHandler = new TakeTaskByUserCommand.Handler(this.context, userManager);
            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<EntityNotFoundException>();
        }
    }
}
