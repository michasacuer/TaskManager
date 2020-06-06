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
    using TaskManager.Tests.WebFunctional.TestData;


    public class AccountControllerTests
    {

        [Fact]
        public async Task ClientCanCreateAccountAndLoginToIt()
        {
            using var testApp = new TestAppClient(new TestSeed());
            
            var command = new RegisterCommand
            {
                UserName = "test",
                FirstName = "test",
                LastName = "test",
                Password = "test11",
                Email = "test@wp.pl",
                Role = Domain.Enum.Role.Manager
            };

            var registerResponse = await testApp.Client.PostAsJsonAsync("Account/Register", command);
            registerResponse.EnsureSuccessStatusCode();

            var query = new LoginQuery
            {
                UserName = "test",
                Password = "test11"
            };

            var loginResponse = await testApp.Client.PostAsJsonAsync("Account/Login", query);
            loginResponse.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task ClientCannotLoginToNotExistedAccount()
        {
            using var testApp = new TestAppClient(new TestSeed());
            
            var query = new LoginQuery
            {
                UserName = "not existed",
                Password = "user"
            };

            await testApp.Client.PostAsJsonAsync("Account/Login", query).ShouldThrowAsync<EntityNotFoundException>();
        }

        [Fact]
        public async Task ClientCantRegisterWhenUserWithSameCredentialExisting()
        {
            using var testApp = new TestAppClient(new TestSeed());
            
            var command = new RegisterCommand
            {
                UserName = "username0",
                FirstName = "test",
                LastName = "test",
                Password = "test11",
                Email = "test@wp.pl",
                Role = Domain.Enum.Role.Manager
            };

            await testApp.Client.PostAsJsonAsync("Account/Register", command).ShouldThrowAsync<EntityAlreadyExistsException>();
        }
    }
}
