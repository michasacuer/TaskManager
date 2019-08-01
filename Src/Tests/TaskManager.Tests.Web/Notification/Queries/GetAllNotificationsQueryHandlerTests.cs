namespace TaskManager.Tests.Web
{
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;
    using Shouldly;
    using TaskManager.Application.Interfaces;
    using TaskManager.Application.Notification.Queries.GetAllNotifications;
    using TaskManager.Domain.Entity;
    using TaskManager.Tests.Infrastructure;
    using TaskManager.Persistence.Repository;

    [Collection("ServicesTestCollection")]
    public class GetAllNotificationsQueryHandlerTests
    {
        private readonly IRepository<Notification> repository;

        public GetAllNotificationsQueryHandlerTests(ServicesFixture fixture)
        {
            this.repository = new Repository<Notification>(fixture.Context);
        }

        [Fact]
        public async Task GetAllNotificationsFromDatabase()
        {
            var queryHandler = new GetAllNotificationsQueryHandler(this.repository);

            var result = await queryHandler.Handle(new GetAllNotificationsQuery(), CancellationToken.None);

            result.ShouldBeOfType<NotificationsModel>();
        }
    }
}
