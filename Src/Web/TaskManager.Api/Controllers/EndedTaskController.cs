namespace TaskManager.Api.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TaskManager.Application.EndedTask.Queries.GetAllEndedTasks;

    public class EndedTaskController : BaseController
    {
        /// <summary>
        /// Get all ended tasks
        /// </summary>
        /// <returns>All ended tasks</returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<EndedTasksModel>> GetAllTasks()
        {
            return Ok(await base.Mediator.Send(new GetAllEndedTasksQuery()));
        }
    }
}
