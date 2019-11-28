namespace TaskManager.Tests.Web
{
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation;
    using Microsoft.EntityFrameworkCore;
    using Shouldly;
    using Xunit;
    using TaskManager.Application.Commands.CreateProject;
    using TaskManager.Application.Interfaces;
    using TaskManager.Persistence;
    using TaskManager.Persistence.Repository;
    using TaskManager.Tests.Infrastructure;

    [Collection("ServicesTestCollection")]
    public class CreateProjectCommandTests
    {
        private readonly TaskManagerDbContext context;

        private readonly ProjectRepository projectRepository;

        private readonly INotificationService notificationService;

        public CreateProjectCommandTests(ServicesFixture fixture)
        {
            this.context = fixture.Context;
            this.projectRepository = new ProjectRepository(fixture.Context);
            this.notificationService = fixture.NotificationService;
        }

        [Fact]
        public async Task CreateProjectShouldAddProjectToDatabase()
        {
            var command = new CreateProjectCommand
            {
                Name = "Project",
                Description = "Description"
            };

            var commandHandler = new CreateProjectCommand.Handler(this.projectRepository, this.notificationService);

            await commandHandler.Handle(command, CancellationToken.None);

            var project = await this.context.Projects.FirstOrDefaultAsync(p => p.Name.Equals(command.Name));

            project.ShouldNotBeNull();
            project.Name.ShouldBe(command.Name);
        }

        [Fact]
        public async Task CreateProjectWithoutDescriptionShouldAddProjectToDatabase()
        {
            var command = new CreateProjectCommand
            {
                Name = "ProjectTest",
            };

            var commandHandler = new CreateProjectCommand.Handler(this.projectRepository, this.notificationService);

            await commandHandler.Handle(command, CancellationToken.None);

            var project = await this.context.Projects.FirstOrDefaultAsync(p => p.Name.Equals(command.Name));

            project.ShouldNotBeNull();
            project.Name.ShouldBe(command.Name);
        }

        [Fact]
        public async Task CreateProjectWithEmptyNameShouldThrowException()
        {
            var command = new CreateProjectCommand
            {
                Description = "Description of project without name"
            };

            var commandHandler = new CreateProjectCommand.Handler(this.projectRepository, this.notificationService);

            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<ValidationException>();
        }
    }
}
