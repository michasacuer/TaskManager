namespace TaskManager.Tests.Web
{
    using System.Threading;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;
    using TaskManager.Application.Interfaces;
    using TaskManager.Domain.Entity;
    using TaskManager.Persistence.Repository;
    using TaskManager.Tests.Infrastructure;
    using TaskManager.Application.Task.Queries.GetAllTasks;

    [Collection("ServicesTestCollection")]
    public class GetAllTasksQueryHandlerTests
    {
        private readonly IRepository<ToDoTask> repository;

        public GetAllTasksQueryHandlerTests(ServicesFixture fixture)
        {
            this.repository = new Repository<ToDoTask>(fixture.Context);
        }

        [Fact]
        public async Task GetAllTasksShouldGetAllTasksFromDb()
        {
            var queryHandler = new GetAllTasksQueryHandler(this.repository);

            var result = await queryHandler.Handle(new GetAllTasksQuery(), CancellationToken.None);

            result.ShouldBeOfType<TasksListModel>();
            result.ToDoTasks.ShouldNotBeNull();
            result.ToDoTasks.ShouldNotBeEmpty();
        }
    }
}
