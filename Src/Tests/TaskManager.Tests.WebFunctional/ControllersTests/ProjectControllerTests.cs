namespace TaskManager.Tests.WebFunctional.ControllersTests
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;
    using TaskManager.Application.Project.Queries.GetAllProjects;
    using TaskManager.Tests.WebFunctional.Extensions;
    using TaskManager.Tests.WebFunctional.Infrastructure;
    using TaskManager.Application.Project.Queries.GetProject;

    public class ProjectControllerTests : IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private readonly HttpClient client;

        public ProjectControllerTests(CustomWebApplicationFactory<TestStartup> factory)
        {
            this.client = factory.CreateClient();
        }

        [Fact]
        public async Task ServerShouldReturnAllProjectsFromDb()
        {
            await this.client.GetMockManagerCredential();
            var response = await this.client.GetAsync("Project");

            string json = await response.Content.ReadAsStringAsync();
            var reservations = json.DeserializeObjectFromJson<ProjectsListModel>();

            response.EnsureSuccessStatusCode();
            reservations.ShouldBeOfType<ProjectsListModel>();
            reservations.Projects.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task ServerShouldReturnConcreteProjectFromDb()
        {
            await this.client.GetMockManagerCredential();
            var response = await this.client.GetAsync("Project/6");

            string json = await response.Content.ReadAsStringAsync();
            var reservations = json.DeserializeObjectFromJson<ProjectModel>();

            response.EnsureSuccessStatusCode();
            reservations.ShouldBeOfType<ProjectModel>();
            reservations.Project.Tasks.ShouldNotBeEmpty();
        }
    }
}
