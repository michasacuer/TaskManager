namespace TaskManager.Api.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TaskManager.Application.Commands.CreateProject;
    using TaskManager.Application.Commands.EditProject;
    using TaskManager.Application.Project.Commands.DeleteProject;
    using TaskManager.Application.Project.Queries.GetAllProjects;
    using TaskManager.Application.Project.Queries.GetProject;

    public class ProjectController : BaseController
    {
        /// <summary>
        /// Get all Projects
        /// </summary>
        /// <returns>All projects</returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ProjectsListModel>> GetAllProjects()
        {
            return Ok(await base.Mediator.Send(new GetAllProjectsQuery()));
        }

        /// <summary>
        /// Get project by project ID
        /// </summary>
        /// <param name="projectId">Project ID</param>
        /// <returns>Concrete project by ID</returns>
        [HttpGet("{projectId}")]
        [Authorize]
        public async Task<ActionResult<ProjectModel>> GetProject(int projectId)
        {
            return Ok(await base.Mediator.Send(new GetProjectQuery { ProjectId = projectId }));
        }

        /// <summary>
        /// Create Project
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> CreateProject([FromBody]CreateProjectCommand command)
        {
            return Ok(await base.Mediator.Send(command));
        }

        /// <summary>
        /// Edit existing project
        /// </summary>
        [HttpPost("Edit")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> EditProject([FromBody]EditProjectCommand command)
        {
            return Ok(await base.Mediator.Send(command));
        }

        /// <summary>
        /// Deleting project by ID
        /// </summary>
        /// <param name="projectId">Project ID</param>
        [HttpDelete("{projectId}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteProject(int projectId)
        {
            return Ok(await base.Mediator.Send(new DeleteProjectCommand { ProjectId = projectId }));
        }
    }
}