namespace TaskManager.Tests.Web
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation;
    using Shouldly;
    using Moq;
    using Xunit;
    using TaskManager.Application.Interfaces;
    using TaskManager.Application.Task.Commands.EndTaskByUser;
    using TaskManager.Domain.Entity;
    using TaskManager.Tests.Infrastructure;

    public class EndTaskByUserCommandTests
    {
        private readonly Mock<IRepository<ToDoTask>> taskRepositoryMock;

        private readonly Mock<INotificationService> notificationServiceMock;

        private readonly EndTaskByUserCommand.Handler uut;

        public EndTaskByUserCommandTests()
        {
            this.taskRepositoryMock = new Mock<IRepository<ToDoTask>>();
            this.notificationServiceMock = new Mock<INotificationService>();
            
            this.uut = new EndTaskByUserCommand.Handler(this.taskRepositoryMock.Object, this.notificationServiceMock.Object);
        }

        [Theory]
        [NoRecurseAutoData]
        public void EndTaskByUserCommandShouldSetIsDeletedToTrue(EndTaskByUserCommand command, ToDoTask task)
        {
            ToDoTask updatedTask = null;
            task.Id = command.TaskId;
            task.ApplicationUserId = command.ApplicationUserId;
            task.IsDeleted = false;
            
            this.taskRepositoryMock
                .Setup(x =>
                    x.GetByIdAsync(It.Is<int>(z => z == command.TaskId)))
                .ReturnsAsync(task);

            this.taskRepositoryMock
                .Setup(x =>
                    x.Update(It.Is<ToDoTask>(z => z == task)))
                .Callback<ToDoTask>(x => updatedTask = x);

            this.taskRepositoryMock.Setup(x => x.SaveAsync(CancellationToken.None)).Returns(Task.CompletedTask);
            
            var result = uut.Handle(command, CancellationToken.None).Result;

            result.ShouldNotBeNull();
            updatedTask.ShouldNotBeNull();
            updatedTask.IsDeleted.ShouldBeTrue();
        }

        [Theory]
        [ClassData(typeof(InvalidCommands))]
        public void EndTaskByUserCommandShouldThrowWhenDataNotValid(EndTaskByUserCommand command)
        {
            this.uut.Handle(command, CancellationToken.None).ShouldThrowAsync<ValidationException>();
        }
        
        private class InvalidCommands : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    new EndTaskByUserCommand
                    {
                        TaskId = 1
                    }
                };
                
                yield return new object[]
                {
                    new EndTaskByUserCommand
                    {
                        ApplicationUserId = "test"
                    }
                };
            }
            
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
