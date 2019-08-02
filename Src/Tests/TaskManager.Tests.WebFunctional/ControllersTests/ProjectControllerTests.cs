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
        public async Task Server_Should_Return_All_Projects_From_Db()
        {
            await this.client.GetCredential("username0", "password11");
            var response = await this.client.GetAsync("Project");

            string json = await response.Content.ReadAsStringAsync();
            var reservations = json.DeserializeObjectFromJson<ProjectsListModel>();

            response.EnsureSuccessStatusCode();
            reservations.ShouldBeOfType<ProjectsListModel>();
            reservations.Projects.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task Server_Should_Return_Concrete_Project_From_Db()
        {
            await this.client.GetCredential("username0", "password11");
            var response = await this.client.GetAsync("Project/6");

            string json = await response.Content.ReadAsStringAsync();
            var reservations = json.DeserializeObjectFromJson<ProjectModel>();

            response.EnsureSuccessStatusCode();
            reservations.ShouldBeOfType<ProjectModel>();
            reservations.Project.Tasks.ShouldNotBeEmpty();
        }
    }
}
