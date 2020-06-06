namespace TaskManager.Tests.WebFunctional.ControllersTests
{
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;
    using TaskManager.Common.Exceptions;
    using TaskManager.Tests.WebFunctional.Extensions;
    using TaskManager.Tests.WebFunctional.Infrastructure;
    using TaskManager.Tests.WebFunctional.TestData;

    public class RaportControllerTests
    {
        public async Task ServerShouldGeneratePdfAsStringFromProject()
        {
            using var testApp = new TestAppClient(new TestSeed());
            
            await testApp.Client.GetMockManagerCredential();
            var response = await testApp.Client.GetAsync("Raport/6");

            var json = response.Content.ReadAsStringAsync();
            json.ShouldBeOfType<string>();
            response.EnsureSuccessStatusCode();
        }

        public async Task ServerShouldThrowWhenCantGeneratePdf()
        {
            using var testApp = new TestAppClient(new TestSeed());
            
            await testApp.Client.GetMockManagerCredential();
            var response = await testApp.Client.GetAsync("Raport/1000000").ShouldThrowAsync<EntityNotFoundException>();
        }
    }
}
