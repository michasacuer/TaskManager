namespace TaskManager.Tests.WebFunctional.ControllersTests
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;
    using TaskManager.Application.Commands;
    using TaskManager.Application.Queries;
    using TaskManager.Common.Exceptions;
    using TaskManager.Tests.WebFunctional.Infrastructure;

    public class AccountControllerTests : IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private readonly HttpClient client;

        public AccountControllerTests(CustomWebApplicationFactory<TestStartup> factory)
        {
            this.client = factory.CreateClient();
        }

        [Fact]
        public async Task ClientCanCreateAccountAndLoginToIt()
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

        [Fact]
        public async Task ClientCannotLoginToNotExistedAccount()
        {
            var query = new LoginQuery
            {
                UserName = "not existed",
                Password = "user"
            };

            await this.client.PostAsJsonAsync("Account/Login", query).ShouldThrowAsync<EntityNotFoundException>();
        }

        [Fact]
        public async Task ClientCantRegisterWhenUserWithSameCredentialExisting()
        {
            var command = new RegisterCommand
            {
                UserName = "username0",
                FirstName = "test",
                LastName = "test",
                Password = "test11",
                Email = "test@wp.pl",
                Role = Domain.Enum.Role.Manager
            };

            await this.client.PostAsJsonAsync("Account/Register", command).ShouldThrowAsync<EntityAlreadyExistsException>();
        }
    }
}
