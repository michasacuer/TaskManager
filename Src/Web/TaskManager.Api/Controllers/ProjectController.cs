namespace TaskManager.Api.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using TaskManager.Application.Commands;
    using TaskManager.Application.Project.Queries.GetAllProjects;

    public class ProjectController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody]CreateProjectCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet]
        public async Task<ActionResult<ProjectsListViewModel>> GetAllProjects()
        {
            return Ok(await Mediator.Send(new GetAllProjectsQuery()));
        }
    }
}