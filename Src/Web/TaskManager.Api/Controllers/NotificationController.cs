﻿namespace TaskManager.Api.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TaskManager.Application.Notification.Queries.GetAllNotifications;

    public class NotificationController : BaseController
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<NotificationsModel>> GetAllNotifications()
        {
            return Ok(await base.Mediator.Send(new GetAllNotificationsQuery()));
        }
    }
}
