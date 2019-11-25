namespace TaskManager.Tests.WebFunctional.ControllersTests
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;
    using TaskManager.Application.Notification.Queries.GetAllNotifications;
    using TaskManager.Tests.WebFunctional.Extensions;
    using TaskManager.Tests.WebFunctional.Infrastructure;

    public class NotificationControllerTests : IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private readonly HttpClient client;

        public NotificationControllerTests(CustomWebApplicationFactory<TestStartup> factory)
        {
            this.client = factory.CreateClient();
        }

        [Fact]
        public async Task ServerShouldReturnAllNotificationsFromDb()
        {
            await this.client.GetMockManagerCredential();
            var response = await this.client.GetAsync("Notification");

            string json = await response.Content.ReadAsStringAsync();
            var notifications = json.DeserializeObjectFromJson<NotificationsModel>();

            response.EnsureSuccessStatusCode();
            notifications.ShouldBeOfType<NotificationsModel>();
        }
    }
}
