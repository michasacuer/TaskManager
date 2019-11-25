namespace TaskManager.Tests.WebFunctional.ControllersTests
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;
    using TaskManager.Application.Task.Commands.EndTaskByUser;
    using TaskManager.Application.Task.Commands.TakeTaskByUser;
    using TaskManager.Application.Task.Queries.GetAllTasks;
    using TaskManager.Common.Exceptions;
    using TaskManager.Domain.Entity;
    using TaskManager.Domain.Enum;
    using TaskManager.Tests.WebFunctional.Extensions;
    using TaskManager.Tests.WebFunctional.Infrastructure;

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
            tasks.List.ShouldNotBeEmpty();
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

        [Fact]
        public async Task ServerShouldReturnOkAfterAddingTaskToDb()
        {
            await this.client.GetMockManagerCredential();
            var response = await this.client.PostAsJsonAsync("Task", new ToDoTask
            {
                Name = "TaskTest",
                Description = "TestTask",
                Priority = Priority.High,
                ProjectId = 2
            }); 

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task ServerShouldReturnEntityNotFoundExceptionWhenWePassWrongProjectId()
        {
            await this.client.GetMockManagerCredential();
            await this.client.PostAsJsonAsync("Task", new ToDoTask
            {
                Name = "DDD",
                Description = "DD",
                Priority = Priority.High,
                ProjectId = 2000000
            }
            ).ShouldThrowAsync<EntityNotFoundException>();
        }

        [Fact]
        public async Task UserShouldTakeTaskAndEndItWithOkStatusCode()
        {
            var user = await this.client.GetMockManagerCredentialWithUserInfo();
            var response = await this.client.PostAsJsonAsync("Task/TakeTask", new TakeTaskByUserCommand
            {
                ApplicationUserId = user.Id,
                TaskId = 1
            });

            response.EnsureSuccessStatusCode();

            Thread.Sleep(100);

            response = await this.client.PostAsJsonAsync("Task/EndTask", new EndTaskByUserCommand
            {
                ApplicationUserId = user.Id,
                TaskId = 1
            });

            response.EnsureSuccessStatusCode();
        }
    }
}
