namespace TaskManager.Api.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using TaskManager.Application.Commands;

    public class ProjectController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody]CreateProjectCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}