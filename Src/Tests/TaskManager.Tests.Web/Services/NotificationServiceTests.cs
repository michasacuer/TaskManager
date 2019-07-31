namespace TaskManager.Tests.Web.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using Moq;
    using Shouldly;
    using Xunit;
    using TaskManager.Application.Interfaces;
    using TaskManager.Domain.Entity;
    using TaskManager.Persistence.Repository;
    using TaskManager.Tests.Infrastructure;
    using TaskManager.Tests.Web.Infrastructure.Implementations;

    [Collection("ServicesTestCollection")]
    public class NotificationServiceTests
    {
        private readonly ITaskManagerDbContext context;
        private readonly INotificationService service;

        public NotificationServiceTests(ServicesFixture fixture)
        {
            this.context = fixture.Context;

            this.service = new NotificationTestService(new Repository<Notification>(fixture.Context));
        }

        [Fact]
        public async Task SentMessageSaveToDatabaseAsNotification()
        {
            await service.SendMessageToAll("Service Works!");
            var message = this.context.Notifications.FirstOrDefault(m => m.Message.Equals("Service Works!"));

            message.Message.ShouldBe("Service Works!");
        }
    }
}
