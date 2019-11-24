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
    using TaskManager.Domain.Entity;

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
            var projects = json.DeserializeObjectFromJson<ProjectsListModel>();

            response.EnsureSuccessStatusCode();
            projects.ShouldBeOfType<ProjectsListModel>();
            projects.List.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task ServerShouldReturnConcreteProjectFromDb()
        {
            await this.client.GetMockManagerCredential();
            var response = await this.client.GetAsync("Project/6");

            string json = await response.Content.ReadAsStringAsync();
            var project = json.DeserializeObjectFromJson<ProjectModel>();

            response.EnsureSuccessStatusCode();
            project.ShouldBeOfType<ProjectModel>();
            project.Project.Tasks.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task ServerShouldReturnOkAfterAddingProjectToDb()
        {
            await this.client.GetMockManagerCredential();
            var response = await this.client.PostAsJsonAsync("Project", new Project
            {
                Name = "DDD",
                Description = "ddd"
            });

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task ServerShouldReturnOkAfterDeletingProjectFromDb()
        {
            await this.client.GetMockManagerCredential();
            var response = await this.client.DeleteAsync("Project/1");

            response.EnsureSuccessStatusCode();
        }
    }
}
