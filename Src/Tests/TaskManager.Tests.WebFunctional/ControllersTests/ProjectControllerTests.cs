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
    using TaskManager.Tests.WebFunctional.TestData;


    public class ProjectControllerTests 
    {
        [Fact]
        public async Task ServerShouldReturnAllProjectsFromDb()
        {
            using var testApp = new TestAppClient(new TestSeed());
            
            await testApp.Client.GetMockManagerCredential();
            var response = await testApp.Client.GetAsync("Project");

            string json = await response.Content.ReadAsStringAsync();
            var projects = json.DeserializeObjectFromJson<ProjectsListModel>();

            response.EnsureSuccessStatusCode();
            projects.ShouldBeOfType<ProjectsListModel>();
            projects.List.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task ServerShouldReturnConcreteProjectFromDb()
        {
            using var testApp = new TestAppClient(new TestSeed());
            
            await testApp.Client.GetMockManagerCredential();
            var response = await testApp.Client.GetAsync("Project/6");

            string json = await response.Content.ReadAsStringAsync();
            var project = json.DeserializeObjectFromJson<ProjectModel>();

            response.EnsureSuccessStatusCode();
            project.ShouldBeOfType<ProjectModel>();
            project.Project.Tasks.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task ServerShouldReturnOkAfterAddingProjectToDb()
        {
            using var testApp = new TestAppClient(new TestSeed());
            
            await testApp.Client.GetMockManagerCredential();
            var response = await testApp.Client.PostAsJsonAsync("Project", new Project
            {
                Name = "DDD",
                Description = "ddd"
            });

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task ServerShouldReturnOkAfterDeletingProjectFromDb()
        {
            using var testApp = new TestAppClient(new TestSeed());
            
            await testApp.Client.GetMockManagerCredential();
            var response = await testApp.Client.DeleteAsync("Project/1");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task ServerShouldReturnOkAfterEditingProjectInDb()
        {
            using var testApp = new TestAppClient(new TestSeed());
            
            await testApp.Client.GetMockManagerCredential();
            var project = new Project
            {
                Id = 2,
                Name = "DDD",
                Description = "ddd"
            };

            var response = await testApp.Client.PostAsJsonAsync("Project/Edit", new { data = project } );

            response.EnsureSuccessStatusCode();
        }
    }
}
