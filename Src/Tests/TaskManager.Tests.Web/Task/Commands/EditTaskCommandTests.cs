namespace TaskManager.Tests.Web
{
    using System.Threading;
    using FluentValidation;
    using Moq;
    using Shouldly;
    using Xunit;
    using TaskManager.Application.Interfaces;
    using TaskManager.Tests.Infrastructure;
    using TaskManager.Domain.Entity;
    using TaskManager.Common.Exceptions;
    using TaskManager.Application.Task.Commands.EditTask;

    public class EditTaskCommandTests
    {
        private readonly Mock<ITaskRepository> taskRepositoryMock;

        private readonly EditTaskCommand.Handler uut;

        public EditTaskCommandTests()
        {
            this.taskRepositoryMock = new Mock<ITaskRepository>();
            this.uut = new EditTaskCommand.Handler(this.taskRepositoryMock.Object);
        }

        [Theory]
        [NoRecurseAutoData]
        public void EditTaskShouldThrowWhenTaskNotFound(EditTaskCommand command, ToDoTask task)
        {
            task = null;

            this.taskRepositoryMock
                .Setup(x =>
                    x.GetByIdAsync(It.Is<int>(z => z == command.Data.Id)))
                .ReturnsAsync(task);
            
            this.uut.Handle(command, CancellationToken.None).ShouldThrowAsync<EntityNotFoundException>();
        }

        [Theory]
        [NoRecurseAutoData]
        public void EditTaskShouldThrowWhenTaskNameIsEmpty(EditTaskCommand command)
        {
            command.Data.Name = null;

            this.uut.Handle(command, CancellationToken.None).ShouldThrowAsync<ValidationException>();
        }
    }
}
