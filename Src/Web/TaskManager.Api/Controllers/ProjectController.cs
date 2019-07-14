namespace TaskManager.Api.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using TaskManager.Application.Commands.CreateProject;
    using TaskManager.Application.Project.Commands.DeleteProject;
    using TaskManager.Application.Project.Queries.GetAllProjects;
    using TaskManager.Application.Project.Queries.GetProject;

    public class ProjectController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<ProjectsListModel>> GetAllProjects()
        {
            return Ok(await base.Mediator.Send(new GetAllProjectsQuery()));
        }

        [HttpGet("{projectId}")]
        public async Task<ActionResult<ProjectModel>> GetProject(int projectId)
        {
            return Ok(await base.Mediator.Send(new GetProjectQuery { ProjectId = projectId }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody]CreateProjectCommand command)
        {
            return Ok(await base.Mediator.Send(command));
        }

        [HttpDelete("projectId")]
        public async Task<IActionResult> DeleteProject(int projectId)
        {
            return Ok(await base.Mediator.Send(new DeleteProjectCommand { ProjectId = projectId }));
        }
    }
}