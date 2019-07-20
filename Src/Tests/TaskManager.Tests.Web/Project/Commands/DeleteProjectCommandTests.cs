namespace TaskManager.Tests.Web
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;
    using TaskManager.Application.Project.Commands.DeleteProject;
    using TaskManager.Common.Exceptions;
    using TaskManager.Persistence;
    using TaskManager.Persistence.Repository;
    using TaskManager.Tests.Infrastructure;

    [Collection("ServicesTestCollection")]
    public class DeleteProjectCommandTests
    {
        private readonly TaskManagerDbContext context;

        private readonly ProjectRepository projectRepository;

        public DeleteProjectCommandTests(ServicesFixture fixture)
        {
            this.context = fixture.Context;
            this.projectRepository = new ProjectRepository(fixture.Context);
        }

        [Fact]
        public async Task DeleteProjectCommandShouldDeleteConcreteProjectFromDb()
        {
            var project = await this.context.Projects.FindAsync(2);

            var command = new DeleteProjectCommand { ProjectId = project.Id };
            var commandHandler = new DeleteProjectCommand.Handler(this.projectRepository);

            await commandHandler.Handle(command, CancellationToken.None);

            var deletedProject = this.context.Projects.FirstOrDefault(p => p.Id == 22);
            deletedProject.ShouldBe(null);
        }

        [Fact]
        public async Task DeleteProjectShouldThrowWhenProjectNotFound()
        {
            var command = new DeleteProjectCommand { ProjectId = 2323223 };
            var commandHandler = new DeleteProjectCommand.Handler(this.projectRepository);

            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<EntityNotFoundException>();
        }
    }
}
