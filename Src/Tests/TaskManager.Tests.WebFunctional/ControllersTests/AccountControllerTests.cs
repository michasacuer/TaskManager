namespace TaskManager.Tests.WebFunctional.ControllersTests
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xunit;
    using TaskManager.Application.Commands;
    using TaskManager.Application.Queries;
    using TaskManager.Tests.WebFunctional.Infrastructure;

    public class AccountControllerTests : IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private readonly HttpClient client;

        public AccountControllerTests(CustomWebApplicationFactory<TestStartup> factory)
        {
            this.client = factory.CreateClient();
        }

        [Fact]
        public async Task Client_Cant_Create_Account_And_Login_To_It()
        {
            var command = new RegisterCommand
            {
                UserName = "test",
                FirstName = "test",
                LastName = "test",
                Password = "test11",
                Email = "test@wp.pl",
                Role = Domain.Enum.Role.Manager
            };
            var registerResponse = await this.client.PostAsJsonAsync("Account/Register", command);
            registerResponse.EnsureSuccessStatusCode();

            var query = new LoginQuery
            {
                UserName = "test",
                Password = "test11"
            };
            var loginResponse = await this.client.PostAsJsonAsync("Account/Login", query);
            loginResponse.EnsureSuccessStatusCode();
        }
    }
}
