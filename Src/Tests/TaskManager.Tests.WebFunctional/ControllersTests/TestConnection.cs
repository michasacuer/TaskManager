namespace TaskManager.Tests.WebFunctional.ControllersTests
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xunit;
    using TaskManager.Tests.WebFunctional.Infrastructure;

    public class TestConnection : IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private readonly HttpClient client;

        public TestConnection(CustomWebApplicationFactory<TestStartup> factory)
        {
            client = factory.CreateClient();
        }

        [Fact]
        public async Task Check_If_Api_Works()
        {
            var response = await this.client.GetAsync("/api/test");
            response.EnsureSuccessStatusCode();
        }
    }
}
