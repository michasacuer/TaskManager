﻿namespace TaskManager.Tests.Web
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;
    using TaskManager.Application.Project.Queries.GetAllProjects;
    using TaskManager.Persistence;
    using TaskManager.Persistence.Repository;
    using TaskManager.Tests.Infrastructure;

    [Collection("ServicesTestCollection")]
    public class GetAllProjectsQueryHandlerTests
    {
        private readonly TaskManagerDbContext context;

        private readonly ProjectRepository projectRepository;

        public GetAllProjectsQueryHandlerTests(ServicesFixture fixture)
        {
            this.context = fixture.Context;
            this.projectRepository = new ProjectRepository(fixture.Context);
        }

        [Fact]
        public async Task GetAllProjectsShouldGetAllProjectsFromDb()
        {
            var queryHandler = new GetAllProjectsQueryHandler(this.projectRepository);

            var result = await queryHandler.Handle(new GetAllProjectsQuery(), CancellationToken.None);

            result.ShouldBeOfType<ProjectsListModel>();
            result.Projects.ToList().Count.ShouldBe(7);
        }

        [Fact]
        public async Task EnsureThatProjectsHaveIncludedTasks()
        {
            var queryHandler = new GetAllProjectsQueryHandler(this.projectRepository);

            var result = await queryHandler.Handle(new GetAllProjectsQuery(), CancellationToken.None);

            result.ShouldBeOfType<ProjectsListModel>();
            var projects = result.Projects.ToList();

            projects[5].Tasks.ShouldNotBeNull();
        }
    }
}
