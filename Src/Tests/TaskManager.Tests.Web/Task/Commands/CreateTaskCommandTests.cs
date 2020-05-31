namespace TaskManager.Tests.Web
{
    using System.Threading;
    using FluentValidation;
    using Moq;
    using Shouldly;
    using Xunit;
    using TaskManager.Application.Interfaces;
    using TaskManager.Application.Task.Commands.CreateTask;
    using TaskManager.Common.Exceptions;
    using TaskManager.Domain.Entity;
    using TaskManager.Tests.Infrastructure;

    public class CreateTaskCommandTests
    {
        private readonly Mock<IRepository<ToDoTask>> taskRepositoryMock;

        private readonly Mock<IProjectRepository> projectRepositoryMock;

        private readonly Mock<INotificationService> notificationServiceMock;
        
        private readonly CreateTaskCommand.Handler uut;

        public CreateTaskCommandTests()
        {
            this.taskRepositoryMock = new Mock<IRepository<ToDoTask>>();
            this.projectRepositoryMock = new Mock<IProjectRepository>();
            this.notificationServiceMock = new Mock<INotificationService>();
            
            this.uut = new CreateTaskCommand.Handler(
                taskRepositoryMock.Object,
                projectRepositoryMock.Object,
                notificationServiceMock.Object);
        }

        [Theory]
        [NoRecurseAutoData]
        public void CreateTaskThrowExceptionWhenNameIsNull(CreateTaskCommand command)
        {
            command.Name = null;
            
            this.uut.Handle(command, CancellationToken.None).ShouldThrowAsync<ValidationException>();
        }

        [Theory]
        [NoRecurseAutoData]
        public void CreateTaskThrowExceptionWhenProjectNotFound(CreateTaskCommand command, Project project)
        {
            project = null;
            this.projectRepositoryMock
                .Setup(x => 
                    x.GetByIdAsync(It.Is<int>(z => z == command.ProjectId)))
                .ReturnsAsync(project);

            this.uut.Handle(command, CancellationToken.None).ShouldThrowAsync<EntityNotFoundException>();
        }
    }
}
