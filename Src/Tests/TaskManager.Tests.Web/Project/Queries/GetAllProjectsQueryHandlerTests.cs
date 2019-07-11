namespace TaskManager.Tests.Web.Project.Queries
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;
    using TaskManager.Application.Project.Queries.GetAllProjects;
    using TaskManager.Persistence;
    using TaskManager.Tests.Infrastructure;

    [Collection("DatabaseTestCollection")]
    public class GetAllProjectsQueryHandlerTests
    {
        private readonly TaskManagerDbContext context;

        public GetAllProjectsQueryHandlerTests()
        {
            this.context = DatabaseContextFactory.Create();
        }

        [Fact]
        public async Task GetAllProjectsShouldGetAllProjectsFromDb()
        {
            var queryHandler = new GetAllProjectsQueryHandler(this.context);

            var result = await queryHandler.Handle(new GetAllProjectsQuery(), CancellationToken.None);

            result.ShouldBeOfType<ProjectsListViewModel>();
            result.Projects.ToList().Count.ShouldBe(2);
        }
    }
}
