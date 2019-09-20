namespace TaskManager.Tests.WebFunctional.ControllersTests
{
    using System.Net.Http;
    using Xunit;
    using TaskManager.Tests.WebFunctional.Infrastructure;

    public class TaskControllersTests : IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private readonly HttpClient client;

        public TaskControllersTests(CustomWebApplicationFactory<TestStartup> factory)
        {
            this.client = factory.CreateClient();
        }


    }
}
