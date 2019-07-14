namespace TaskManager.Api.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using TaskManager.Application.Commands.CreateProject;
    using TaskManager.Application.Project.Queries.GetAllProjects;

    public class ProjectController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody]CreateProjectCommand command)
        {
            return Ok(await base.Mediator.Send(command));
        }

        [HttpGet]
        public async Task<ActionResult<ProjectsListModel>> GetAllProjects()
        {
            return Ok(await base.Mediator.Send(new GetAllProjectsQuery()));
        }
    }
}