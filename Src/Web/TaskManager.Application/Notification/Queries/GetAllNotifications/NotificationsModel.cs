namespace TaskManager.Application.Notification.Queries.GetAllNotifications
{
    using System.Collections.Generic;
    using TaskManager.Domain.Entity;

    public class NotificationsModel
    {
        public IEnumerable<Notification> List { get; set; }
    }
}
