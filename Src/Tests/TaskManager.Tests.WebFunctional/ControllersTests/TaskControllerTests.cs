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
    using TaskManager.Tests.WebFunctional.TestData;

    public class TaskControllerTests : IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        [Fact]
        public async Task ServerShouldReturnAllTasksFromDb()
        {
            using var testApp = new TestAppClient(new TestSeed());
            
            await testApp.Client.GetMockManagerCredential();
            var response = await testApp.Client.GetAsync("Task");

            string json = await response.Content.ReadAsStringAsync();
            var tasks = json.DeserializeObjectFromJson<TasksListModel>();

            response.EnsureSuccessStatusCode();
            tasks.ShouldBeOfType<TasksListModel>();
            tasks.List.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task ServerShouldReturnConcreteTaskFromDb()
        {
            using var testApp = new TestAppClient(new TestSeed());
            
            await testApp.Client.GetMockManagerCredential();
            var response = await testApp.Client.GetAsync("Task/1");

            string json = await response.Content.ReadAsStringAsync();
            var task = json.DeserializeObjectFromJson<ToDoTask>();

            response.EnsureSuccessStatusCode();
            task.ShouldBeOfType<ToDoTask>();
            task.Name.ShouldNotBeNull();
        }

        [Fact]
        public async Task ServerShouldReturnOkAfterAddingTaskToDb()
        {
            using var testApp = new TestAppClient(new TestSeed());
            
            await testApp.Client.GetMockManagerCredential();
            var response = await testApp.Client.PostAsJsonAsync("Task", new ToDoTask
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
            using var testApp = new TestAppClient(new TestSeed());
            
            await testApp.Client.GetMockManagerCredential();
            await testApp.Client.PostAsJsonAsync("Task", new ToDoTask
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
            using var testApp = new TestAppClient(new TestSeed());
            
            var user = await testApp.Client.GetMockManagerCredentialWithUserInfo();
            var response = await testApp.Client.PostAsJsonAsync("Task/TakeTask", new TakeTaskByUserCommand
            {
                ApplicationUserId = user.Id,
                TaskId = 1
            });

            response.EnsureSuccessStatusCode();

            Thread.Sleep(100);

            response = await testApp.Client.PostAsJsonAsync("Task/EndTask", new EndTaskByUserCommand
            {
                ApplicationUserId = user.Id,
                TaskId = 1
            });

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task ServerShouldReturnOkWhenEditTask()
        {
            using var testApp = new TestAppClient(new TestSeed());
            
            await testApp.Client.GetMockManagerCredential();
            var task = new ToDoTask
            {
                Id = 2,
                Name = "AAAAA",
                Description = "AAAAAA"
            };

            var response = await testApp.Client.PostAsJsonAsync("Task/Edit", new { data = task });

            response.EnsureSuccessStatusCode();
        }
    }
}
