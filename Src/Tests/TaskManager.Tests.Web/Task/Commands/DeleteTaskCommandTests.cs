namespace TaskManager.Tests.Web
{
    using System.Threading;
    using Moq;
    using Shouldly;
    using Xunit;
    using TaskManager.Application.Interfaces;
    using TaskManager.Application.Task.Commands.DeleteTask;
    using TaskManager.Common.Exceptions;
    using TaskManager.Domain.Entity;
    using TaskManager.Tests.Infrastructure;

    public class DeleteTaskCommandTests
    {
        private readonly Mock<IRepository<ToDoTask>> taskRepository;

        private readonly DeleteTaskCommand.Handler uut;

        public DeleteTaskCommandTests()
        {
            this.taskRepository = new Mock<IRepository<ToDoTask>>();
            this.uut = new DeleteTaskCommand.Handler(this.taskRepository.Object);
        }

        [Theory]
        [NoRecurseAutoData]
        public void DeleteTaskShouldThrowWhenTaskNotFound(DeleteTaskCommand command, ToDoTask task)
        {
            task = null;
            this.taskRepository.Setup(x => x.GetByIdAsync(It.Is<int>(z => z == command.TaskId))).ReturnsAsync(task);
            
            this.uut.Handle(command, CancellationToken.None).ShouldThrowAsync<EntityNotFoundException>();
        }
    }
}
