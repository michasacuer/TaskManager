namespace TaskManager.Tests.WebFunctional.ControllersTests
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;
    using TaskManager.Common.Exceptions;
    using TaskManager.Tests.WebFunctional.Extensions;
    using TaskManager.Tests.WebFunctional.Infrastructure;

    public class RaportControllerTests : IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private readonly HttpClient client;

        public RaportControllerTests(CustomWebApplicationFactory<TestStartup> factory)
        {
            this.client = factory.CreateClient();
        }

        public async Task ServerShouldGeneratePdfAsStringFromProject()
        {
            await this.client.GetMockManagerCredential();
            var response = await this.client.GetAsync("Raport/6");

            var json = response.Content.ReadAsStringAsync();
            json.ShouldBeOfType<string>();
            response.EnsureSuccessStatusCode();
        }

        public async Task ServerShouldThrowWhenCantGeneratePdf()
        {
            await this.client.GetMockManagerCredential();
            var response = await this.client.GetAsync("Raport/1000000").ShouldThrowAsync<EntityNotFoundException>();
        }
    }
}
