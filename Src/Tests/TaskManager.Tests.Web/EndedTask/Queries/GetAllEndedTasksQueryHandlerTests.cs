namespace TaskManager.Tests.Web
{
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;
    using Shouldly;
    using TaskManager.Application.Interfaces;
    using TaskManager.Domain.Entity;
    using TaskManager.Tests.Infrastructure;
    using TaskManager.Persistence.Repository;
    using TaskManager.Application.EndedTask.Queries.GetAllEndedTasks;

    [Collection("ServicesTestCollection")]
    public class GetAllEndedTasksQueryHandlerTests
    {
        private readonly IRepository<EndedTask> repository;

        public GetAllEndedTasksQueryHandlerTests(ServicesFixture fixture)
        {
            this.repository = new Repository<EndedTask>(fixture.Context);
        }

        [Fact]
        public async Task GetAllEndedTaskQuerySHouldReturnModelWithEndedTasksList()
        {
            var queryHandler = new GetAllEndedTasksQueryHandler(this.repository);

            var result = await queryHandler.Handle(new GetAllEndedTasksQuery(), CancellationToken.None);

            result.ShouldBeOfType<EndedTasksModel>();
        }
    }
}
