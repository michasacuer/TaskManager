namespace TaskManager.Tests.Web
{
    using System.Threading;
    using System.Threading.Tasks;
    using Moq;
    using Shouldly;
    using Xunit;
    using TaskManager.Application.Interfaces;
    using TaskManager.Application.Project.Commands.DeleteProject;
    using TaskManager.Common.Exceptions;
    using TaskManager.Domain.Entity;
    using TaskManager.Tests.Infrastructure;

    public class DeleteProjectCommandTests
    {
        private readonly Mock<IProjectRepository> projectRepositoryMock;

        private readonly DeleteProjectCommand.Handler uut;

        public DeleteProjectCommandTests()
        {
            this.projectRepositoryMock = new Mock<IProjectRepository>();
            this.uut = new DeleteProjectCommand.Handler(this.projectRepositoryMock.Object);
        }

        [Theory]
        [NoRecurseAutoData]
        public void DeleteProjectCommandShouldDeleteConcreteProjectFromDb(DeleteProjectCommand command, Project project)
        {
            this.projectRepositoryMock
                .Setup(x => 
                    x.GetByIdAsync(It.Is<int>(z => z == command.ProjectId)))
                .ReturnsAsync(project);
            
            this.projectRepositoryMock.Setup(x => x.Delete(It.Is<Project>(z => z == project)));
            this.projectRepositoryMock.Setup(x => x.SaveAsync(CancellationToken.None)).Returns(Task.CompletedTask);
            
            var result = this.uut.Handle(command, CancellationToken.None).Result;

            result.ShouldNotBeNull();
        }

        [Theory]
        [NoRecurseAutoData]
        public void DeleteProjectShouldThrowWhenProjectNotFound(DeleteProjectCommand command)
        {
            this.uut.Handle(command, CancellationToken.None).ShouldThrowAsync<EntityNotFoundException>();
        }
    }
}
