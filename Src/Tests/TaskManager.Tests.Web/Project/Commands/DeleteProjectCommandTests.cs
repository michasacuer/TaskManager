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
    using TaskManager.Tests.Infrastructure;

    [Collection("DatabaseTestCollection")]
    public class DeleteProjectCommandTests
    {
        private readonly TaskManagerDbContext context;

        public DeleteProjectCommandTests(DatabaseFixture fixture)
        {
            this.context = fixture.Context;
        }

        [Fact]
        public async Task DeleteProjectCommandShouldDeleteConcreteProjectFromDb()
        {
            var project = await this.context.Projects.FindAsync(2);

            var command = new DeleteProjectCommand { ProjectId = project.Id };
            var commandHandler = new DeleteProjectCommand.Handler(this.context);

            await commandHandler.Handle(command, CancellationToken.None);

            var deletedProject = this.context.Projects.FirstOrDefault(p => p.Id == 22);
            deletedProject.ShouldBe(null);
        }

        [Fact]
        public async Task DeleteProjectShouldThrowWhenProjectNotFound()
        {
            var command = new DeleteProjectCommand { ProjectId = 2323223 };
            var commandHandler = new DeleteProjectCommand.Handler(this.context);

            await commandHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<EntityNotFoundException>();
        }
    }
}
