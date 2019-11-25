namespace TaskManager.Tests.Web
{
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation;
    using Shouldly;
    using Xunit;
    using TaskManager.Persistence;
    using TaskManager.Persistence.Repository;
    using TaskManager.Tests.Infrastructure;
    using TaskManager.Application.Commands.EditProject;
    using TaskManager.Domain.Entity;
    using TaskManager.Common.Exceptions;

    [Collection("ServicesTestCollection")]
    public class EditProjectCommandTests
    {
        private readonly TaskManagerDbContext context;

        private readonly ProjectRepository projectRepository;

        public EditProjectCommandTests(ServicesFixture fixture)
        {
            this.context = fixture.Context;
            this.projectRepository = new ProjectRepository(fixture.Context);
        }

        [Fact]
        public async Task EditProjectShouldEditProjectInDatabase()
        {
            var projectBefore = await this.context.Projects.FindAsync(1);
            string nameBefore = projectBefore.Name;

            var command = new EditProjectCommand
            {
                Project = new Project
                {
                    Id = 1,
                    Name = "ChangedName!",
                    Description = "ChangedDesc!"
                }
            };

            var commandHandler = new EditProjectCommand.Handler(this.projectRepository);

            await commandHandler.Handle(command, CancellationToken.None);

            var project = await this.context.Projects.FindAsync(1);

            project.ShouldNotBeNull();
            project.Name.ShouldBe(command.Project.Name);
            nameBefore.ShouldNotBe(project.Name);
        }

        [Fact]
        public async Task EditProjectShouldThrowWhenProjectNotFound()
        {
            var command = new EditProjectCommand
            {
                Project = new Project
                {
                    Id = 1111111111,
                    Name = "ChangedName!",
                    Description = "ChangedDesc!"
                }
            };

            var commandHandler = new EditProjectCommand.Handler(this.projectRepository);

            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<EntityNotFoundException>();
        }

        [Fact]
        public async Task EditProjectShouldThrowWhenProjectNameIsEmpty()
        {
            var command = new EditProjectCommand
            {
                Project = new Project
                {
                    Id = 1,
                    Name = "",
                    Description = "ChangedDesc!"
                }
            };

            var commandHandler = new EditProjectCommand.Handler(this.projectRepository);

            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<ValidationException>();
        }
    }
}