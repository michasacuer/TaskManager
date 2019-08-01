namespace TaskManager.Application.Notification.Queries.GetAllNotifications
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using TaskManager.Application.Interfaces;
    using TaskManager.Domain.Entity;

    public class GetAllNotificationsQueryHandler : IRequestHandler<GetAllNotificationsQuery, NotificationsModel>
    {
        private readonly IRepository<Notification> repository;

        public GetAllNotificationsQueryHandler(IRepository<Notification> repository)
        {
            this.repository = repository;
        }

        public async Task<NotificationsModel> Handle(GetAllNotificationsQuery request, CancellationToken cancellationToken)
        {
            var notifications = await this.repository.GetAllAsync();

            return new NotificationsModel
            {
                Notifications = notifications
            };
        }
    }
}
