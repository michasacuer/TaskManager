namespace TaskManager.Tests.WebFunctional.ControllersTests
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;
    using TaskManager.Tests.WebFunctional.Extensions;
    using TaskManager.Tests.WebFunctional.Infrastructure;
    using TaskManager.Application.Task.Queries.GetAllTasks;
    using TaskManager.Application.Task.Queries.GetUserTask;
    using TaskManager.Domain.Entity;

    public class TaskControllerTests : IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private readonly HttpClient client;

        public TaskControllerTests(CustomWebApplicationFactory<TestStartup> factory)
        {
            this.client = factory.CreateClient();
        }

        [Fact]
        public async Task ServerShouldReturnAllTasksFromDb()
        {
            await this.client.GetMockManagerCredential();
            var response = await this.client.GetAsync("Task");

            string json = await response.Content.ReadAsStringAsync();
            var tasks = json.DeserializeObjectFromJson<TasksListModel>();

            response.EnsureSuccessStatusCode();
            tasks.ShouldBeOfType<TasksListModel>();
            tasks.ToDoTasks.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task ServerShouldReturnConcreteTaskFromDb()
        {
            await this.client.GetMockManagerCredential();
            var response = await this.client.GetAsync("Task/1");

            string json = await response.Content.ReadAsStringAsync();
            var task = json.DeserializeObjectFromJson<ToDoTask>();

            response.EnsureSuccessStatusCode();
            task.ShouldBeOfType<ToDoTask>();
            task.Name.ShouldNotBeNull();
        }
    }
}
