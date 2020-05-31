namespace TaskManager.Tests.Web
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation;
    using Moq;
    using Shouldly;
    using Xunit;
    using TaskManager.Application.Task.Commands.TakeTaskByUser;
    using TaskManager.Application.Interfaces;
    using TaskManager.Common.Exceptions;
    using TaskManager.Domain.Entity;
    using TaskManager.Tests.Infrastructure;

    public class TakeTaskByUserCommandTests
    {
        private readonly Mock<IApplicationUserRepository> applicationUserRepositoryMock;

        private readonly Mock<IRepository<ToDoTask>> taskRepositoryMock;

        private readonly Mock<INotificationService> notificationServiceMock;
        
        private readonly TakeTaskByUserCommand.Handler uut;

        public TakeTaskByUserCommandTests()
        {
            this.applicationUserRepositoryMock = new Mock<IApplicationUserRepository>();
            this.taskRepositoryMock = new Mock<IRepository<ToDoTask>>();
            this.notificationServiceMock = new Mock<INotificationService>();
            
            this.uut = new TakeTaskByUserCommand.Handler(
                this.applicationUserRepositoryMock.Object,
                this.taskRepositoryMock.Object,
                this.notificationServiceMock.Object);
        }

        [Theory]
        [NoRecurseAutoData]
        public void TakeTaskByUserCommandAssignUserIdToTask(TakeTaskByUserCommand command, ApplicationUser user, ToDoTask task)
        {
            ToDoTask updatedTask = null;
            command.ApplicationUserId = user.Id;
            command.TaskId = task.Id;
            task.ApplicationUserId = null;

            this.applicationUserRepositoryMock
                .Setup(x => 
                    x.GetByIdAsync(It.Is<string>(z => z == command.ApplicationUserId)))
                .ReturnsAsync(user);

            this.applicationUserRepositoryMock
                .Setup(x =>
                    x.UserInRoleAsync(It.Is<ApplicationUser>(z => z == user), It.IsAny<string>()))
                .ReturnsAsync(true);

            this.taskRepositoryMock.Setup(x => x.SaveAsync(CancellationToken.None)).Returns(Task.CompletedTask);
            this.taskRepositoryMock.Setup(x => x.GetByIdAsync(It.Is<int>(z => z == command.TaskId))).ReturnsAsync(task);
            this.taskRepositoryMock
                .Setup(x =>
                    x.Update(It.Is<ToDoTask>(z => z == task)))
                .Callback<ToDoTask>(x => updatedTask = x);

            var result = this.uut.Handle(command, CancellationToken.None).Result;

            result.ShouldNotBeNull();
            updatedTask.ShouldNotBeNull();
            updatedTask.ApplicationUserId.ShouldBe(user.Id);
            updatedTask.StartTime.ShouldNotBeNull();
        }

        [Theory]
        [NoRecurseAutoData]
        public void TakeTaskByUserShouldThrowExceptionWhenApplicationUserNotFound(
            TakeTaskByUserCommand command,
            ApplicationUser user)
        {
            user = null;
            this.applicationUserRepositoryMock
                .Setup(x => 
                    x.GetByIdAsync(It.Is<string>(z => z == command.ApplicationUserId)))
                .ReturnsAsync(user);
            
            this.uut.Handle(command, CancellationToken.None).ShouldThrowAsync<EntityNotFoundException>();
        }

        [Theory]
        [NoRecurseAutoData]
        public void TakeTaskByUserShouldThrowExceptionWhenTaskIsAlredyAssignToAnotherUser(
            TakeTaskByUserCommand command,
            ApplicationUser user,
            ToDoTask task)
        {
            task.ApplicationUserId = "not empty";
            
            this.applicationUserRepositoryMock
                .Setup(x => 
                    x.GetByIdAsync(It.Is<string>(z => z == command.ApplicationUserId)))
                .ReturnsAsync(user);

            this.applicationUserRepositoryMock
                .Setup(x =>
                    x.UserInRoleAsync(It.Is<ApplicationUser>(z => z == user), It.IsAny<string>()))
                .ReturnsAsync(true);
            
            this.taskRepositoryMock.Setup(x => x.GetByIdAsync(It.Is<int>(z => z == command.TaskId))).ReturnsAsync(task);
            
            this.uut.Handle(command, CancellationToken.None).ShouldThrowAsync<ValidationException>();
        }

        [Theory]
        [ClassData(typeof(InvalidCommands))]
        public void TakeTaskByUserShouldThrowExceptionWhenCommandInvalid(TakeTaskByUserCommand command)
        {
            this.uut.Handle(command, CancellationToken.None).ShouldThrowAsync<ValidationException>();
        }

        [Theory]
        [NoRecurseAutoData]
        public void TakeTaskByUserShouldThrowExceptionWhenTaskNotFound(
            TakeTaskByUserCommand command,
            ApplicationUser user,
            ToDoTask task)
        {
            task = null;
            
            this.applicationUserRepositoryMock
                .Setup(x => 
                    x.GetByIdAsync(It.Is<string>(z => z == command.ApplicationUserId)))
                .ReturnsAsync(user);

            this.applicationUserRepositoryMock
                .Setup(x =>
                    x.UserInRoleAsync(It.Is<ApplicationUser>(z => z == user), It.IsAny<string>()))
                .ReturnsAsync(true);
            
            this.taskRepositoryMock.Setup(x => x.GetByIdAsync(It.Is<int>(z => z == command.TaskId))).ReturnsAsync(task);
            
            this.uut.Handle(command, CancellationToken.None).ShouldThrowAsync<EntityNotFoundException>();
        }
        
        [Theory]
        [NoRecurseAutoData]
        public void TakeTaskByUserShouldThrowExceptionWhenUserRoleNotValid(
            TakeTaskByUserCommand command,
            ApplicationUser user)
        {
            this.applicationUserRepositoryMock
                .Setup(x => 
                    x.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            this.applicationUserRepositoryMock
                .Setup(x =>
                    x.UserInRoleAsync(It.Is<ApplicationUser>(z => z == user), It.IsAny<string>()))
                .ReturnsAsync(false);
            
            this.uut.Handle(command, CancellationToken.None).ShouldThrowAsync<InvalidUserException>();
        }
        
        private class InvalidCommands : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    new TakeTaskByUserCommand
                    {
                        TaskId = 1
                    }
                };
                
                yield return new object[]
                {
                    new TakeTaskByUserCommand
                    {
                        ApplicationUserId = "test"
                    }
                };
            }
            
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
