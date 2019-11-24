namespace TaskManager.Tests.Web
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;
    using TaskManager.Application.Project.Queries.GetAllProjects;
    using TaskManager.Persistence.Repository;
    using TaskManager.Tests.Infrastructure;

    [Collection("ServicesTestCollection")]
    public class GetAllProjectsQueryHandlerTests
    {
        private readonly ProjectRepository projectRepository;

        public GetAllProjectsQueryHandlerTests(ServicesFixture fixture)
        {
            this.projectRepository = new ProjectRepository(fixture.Context);
        }

        [Fact]
        public async Task GetAllProjectsShouldGetAllProjectsFromDb()
        {
            var queryHandler = new GetAllProjectsQueryHandler(this.projectRepository);

            var result = await queryHandler.Handle(new GetAllProjectsQuery(), CancellationToken.None);

            result.ShouldBeOfType<ProjectsListModel>();
            result.List.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task EnsureThatProjectsHaveIncludedTasks()
        {
            var queryHandler = new GetAllProjectsQueryHandler(this.projectRepository);

            var result = await queryHandler.Handle(new GetAllProjectsQuery(), CancellationToken.None);

            result.ShouldBeOfType<ProjectsListModel>();
            var projects = result.List.ToList();

            projects[5].Tasks.ShouldNotBeNull();
        }
    }
}
