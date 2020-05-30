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
    using TaskManager.Application.Commands.CreateProject;
    using TaskManager.Application.Interfaces;
    using TaskManager.Domain.Entity;
    using TaskManager.Tests.Infrastructure;

    public class CreateProjectCommandTests
    {
        private readonly Mock<IProjectRepository> projectRepository;

        private readonly Mock<INotificationService> notificationService;

        private readonly CreateProjectCommand.Handler uut;

        public CreateProjectCommandTests()
        {
            this.projectRepository = new Mock<IProjectRepository>();
            this.notificationService = new Mock<INotificationService>();
            this.uut = new CreateProjectCommand.Handler(this.projectRepository.Object, this.notificationService.Object);
        }

        [Theory]
        [ClassData(typeof(Commands))]
        public void CreateProjectShouldAddProjectToDatabase(CreateProjectCommand command)
        {
            Project addedProject = null;
            this.projectRepository
                .Setup(x =>
                    x.AddAsync(It.IsAny<Project>()))
                .Callback<Project>(x => addedProject = x)
                .Returns(Task.CompletedTask);
            
            var result = uut.Handle(command, CancellationToken.None).Result;

            result.ShouldNotBeNull();
            addedProject.ShouldNotBeNull();
            addedProject.Name.ShouldBe(command.Name);
            addedProject.Description.ShouldBe(command.Description);
        }

        [Theory]
        [NoRecurseAutoData]
        public void CreateProjectWithEmptyNameShouldThrowException(CreateProjectCommand command)
        {
            command.Name = null;
            uut.Handle(command, CancellationToken.None).ShouldThrowAsync<ValidationException>();
        }

        private class Commands : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    new CreateProjectCommand
                    {
                        Name = "name",
                        Description = "desc",
                    }
                };
                
                yield return new object[]
                {
                    new CreateProjectCommand
                    {
                        Name = "name",
                    }
                };
            }
            
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
