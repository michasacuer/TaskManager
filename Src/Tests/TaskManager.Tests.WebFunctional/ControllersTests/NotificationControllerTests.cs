namespace TaskManager.Tests.WebFunctional.ControllersTests
{
    using TaskManager.Tests.WebFunctional.TestData;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;
    using TaskManager.Application.Notification.Queries.GetAllNotifications;
    using TaskManager.Tests.WebFunctional.Extensions;
    using TaskManager.Tests.WebFunctional.Infrastructure;

    public class NotificationControllerTests 
    {
        public async Task ServerShouldReturnAllNotificationsFromDb()
        {
            using (var testApp = new TestAppClient(new TestSeed()))
            {
                await testApp.Client.GetMockManagerCredential();
                var response = await testApp.Client.GetAsync("Notification");

                string json = await response.Content.ReadAsStringAsync();
                var notifications = json.DeserializeObjectFromJson<NotificationsModel>();

                response.EnsureSuccessStatusCode();
                notifications.ShouldBeOfType<NotificationsModel>();
            }
        }
    }
}
