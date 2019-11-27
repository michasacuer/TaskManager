namespace TaskManager.Tests.WebFunctional.ControllersTests
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;
    using TaskManager.Application.EndedTask.Queries.GetAllEndedTasks;
    using TaskManager.Tests.WebFunctional.Extensions;
    using TaskManager.Tests.WebFunctional.Infrastructure;

    public class EndedTaskControllerTests : IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private readonly HttpClient client;

        public EndedTaskControllerTests(CustomWebApplicationFactory<TestStartup> factory)
        {
            this.client = factory.CreateClient();
        }

        [Fact]
        public async Task ServerShouldReturnAllEndedTasksFromDb()
        {
            await this.client.GetMockManagerCredential();
            var response = await this.client.GetAsync("EndedTask");

            string json = await response.Content.ReadAsStringAsync();
            var tasks = json.DeserializeObjectFromJson<EndedTasksModel>();

            response.EnsureSuccessStatusCode();
            tasks.ShouldBeOfType<EndedTasksModel>();
        }
    }
}
