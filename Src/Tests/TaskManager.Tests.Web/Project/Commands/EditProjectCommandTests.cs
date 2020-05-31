namespace TaskManager.Tests.Web
{
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation;
    using Moq;
    using Shouldly;
    using Xunit;
    using TaskManager.Persistence;
    using TaskManager.Persistence.Repository;
    using TaskManager.Tests.Infrastructure;
    using TaskManager.Application.Commands.EditProject;
    using TaskManager.Application.Interfaces;
    using TaskManager.Domain.Entity;
    using TaskManager.Common.Exceptions;

    public class EditProjectCommandTests
    {
        private readonly Mock<IProjectRepository> projectRepositoryMock;

        private readonly EditProjectCommand.Handler uut;

        public EditProjectCommandTests()
        {
            this.projectRepositoryMock = new Mock<IProjectRepository>();
            this.uut = new EditProjectCommand.Handler(projectRepositoryMock.Object);
        }

        [Theory]
        [NoRecurseAutoData]
        public void EditProjectShouldEditProjectInDatabase(
            EditProjectCommand command,
            Project updatedProject,
            Project projectToUpdate)
        {
            updatedProject.Id = projectToUpdate.Id;
            command.Data.Id = projectToUpdate.Id;
            
            this.projectRepositoryMock
                .Setup(x => 
                    x.GetByIdAsync(It.Is<int>(z => z == command.Data.Id)))
                .ReturnsAsync(projectToUpdate);
            
            this.projectRepositoryMock.Setup(x => x.Update(It.Is<Project>(z => z == updatedProject)));
            this.projectRepositoryMock.Setup(x => x.SaveAsync(CancellationToken.None)).Returns(Task.CompletedTask);

            var result = this.uut.Handle(command, CancellationToken.None).Result;

            result.ShouldNotBeNull();
        }

        [Theory]
        [NoRecurseAutoData]
        public void EditProjectShouldThrowWhenProjectNotFound(
            EditProjectCommand command,
            Project projectToUpdate)
        {
            projectToUpdate = null;
            
            this.projectRepositoryMock
                .Setup(x => 
                    x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(projectToUpdate);
            
            this.uut.Handle(command, CancellationToken.None).ShouldThrowAsync<EntityNotFoundException>();
        }

        [Theory]
        [NoRecurseAutoData]
        public void EditProjectShouldThrowWhenProjectNameIsEmpty(
            EditProjectCommand command,
            Project projectToUpdate)
        {
            command.Data.Name = null;
            this.projectRepositoryMock
                .Setup(x => 
                    x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(projectToUpdate);
            
            this.uut.Handle(command, CancellationToken.None).ShouldThrowAsync<ValidationException>();
        }
    }
}