namespace TaskManager.Tests.WebFunctional.ControllersTests
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR.Client;
    using Microsoft.AspNetCore.TestHost;
    using Shouldly;
    using Xunit;
    using TaskManager.Tests.WebFunctional.Infrastructure;

    public class TestConnection : IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private readonly HttpClient client;

        private readonly TestServer server;

        public TestConnection(CustomWebApplicationFactory<TestStartup> factory)
        {
            this.client = factory.CreateClient();
            this.server = factory.Server;
        }

        [Fact]
        public async Task CheckIfApiWorks()
        {
            var response = await this.client.GetAsync("/api/test");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task CheckIfClientCanConnectToSignalRHub()
        {
            var hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost/Notifications", o =>
                {
                    o.HttpMessageHandlerFactory = _ => this.server.CreateHandler();
                })
                .Build();

            await hubConnection.StartAsync();

            const string Message = "SignalR Works!";
            string response = string.Empty;

            hubConnection.On<string>("ReciveMessage", (message) => response = message);
            await hubConnection.InvokeAsync("SendMessage", Message);

            response.ShouldBe(Message);
        }
    }
}
