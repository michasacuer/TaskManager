namespace TaskManager.Tests.WebFunctional.ControllersTests
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xunit;
    using TaskManager.Tests.WebFunctional.Infrastructure;

    public class TestConnection : IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private readonly HttpClient _client;

        public TestConnection(CustomWebApplicationFactory<TestStartup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Check_If_Api_Works()
        {
            var response = await _client.GetAsync("/api/test");
            response.EnsureSuccessStatusCode();
        }
    }
}
