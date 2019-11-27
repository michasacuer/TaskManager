namespace TaskManager.Tests.Web
{
    using System.Threading;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;
    using TaskManager.Application.Interfaces;
    using TaskManager.Application.Task.Queries.GetUserTask;
    using TaskManager.Common.Exceptions;
    using TaskManager.Persistence.Repository;
    using TaskManager.Tests.Infrastructure;
    using TaskManager.Domain.Entity;

    [Collection("ServicesTestCollection")]
    public class GetUserTaskQueryTests
    {
        private readonly ITaskRepository taskRepository;

        public GetUserTaskQueryTests(ServicesFixture fixture)
        {
            this.taskRepository = new TaskRepository(fixture.Context);
        }

        [Fact]
        public async Task GetUserTaskShouldReturnTaskThatUserIsWorkingOn()
        {
            var command = new GetUserTaskQuery
            {
                ApplicationUserId = "GetUserTaskTest"
            };

            var queryHandler = new GetUserTaskQueryHandler(this.taskRepository);

            var result = await queryHandler.Handle(command, CancellationToken.None);

            result.ShouldBeOfType<ToDoTask>();
            result.ShouldNotBeNull();
        }

        [Fact]
        public async Task ThrowExceptionWhenIdIsNull()
        {
            var command = new GetUserTaskQuery();

            var queryHandler = new GetUserTaskQueryHandler(this.taskRepository);

            var result = await queryHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<EntityNotFoundException>();
        }

        [Fact]
        public async Task ThrowExceptionWhenTaskIsNotFound()
        {
            var command = new GetUserTaskQuery
            {
                ApplicationUserId = "There is no such id"
            };

            var queryHandler = new GetUserTaskQueryHandler(this.taskRepository);

            var result = await queryHandler.Handle(command, CancellationToken.None).ShouldThrowAsync<EntityNotFoundException>();
        }
    }
}
