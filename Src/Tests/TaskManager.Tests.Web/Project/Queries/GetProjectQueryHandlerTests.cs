namespace TaskManager.Tests.Web
{
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation;
    using Shouldly;
    using Xunit;
    using TaskManager.Application.Project.Queries.GetProject;
    using TaskManager.Common.Exceptions;
    using TaskManager.Persistence;
    using TaskManager.Tests.Infrastructure;

    [Collection("DatabaseTestCollection")]
    public class GetProjectQueryHandlerTests
    {
        private readonly TaskManagerDbContext context;

        public GetProjectQueryHandlerTests(DatabaseFixture fixture)
        {
            this.context = fixture.Context;
        }

        [Fact]
        public async Task GetProjectShouldReturnConcreteProjectById()
        {
            var command = new GetProjectQuery
            {
                ProjectId = 2
            };

            var queryHandler = new GetProjectQueryHandler(this.context);

            var result = await queryHandler.Handle(command, CancellationToken.None);

            result.ShouldBeOfType<ProjectModel>();
            result.Project.ShouldNotBeNull();
        }

        [Fact]
        public async Task GetProjectThrowExceptionWhenProjectNotFound()
        {
            var command = new GetProjectQuery
            {
                ProjectId = 32132134
            };

            var queryHandler = new GetProjectQueryHandler(this.context);

            await queryHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<EntityNotFoundException>();
        }

        [Fact]
        public async Task GetProjectThrowExceptionWhenProjectIdIsEmpty()
        {
            var queryHandler = new GetProjectQueryHandler(this.context);

            await queryHandler.Handle(new GetProjectQuery(), CancellationToken.None).ShouldThrowAsync<ValidationException>();
        }
    }
}
