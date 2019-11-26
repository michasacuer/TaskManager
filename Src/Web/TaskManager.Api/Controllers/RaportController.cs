namespace TaskManager.Api.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TaskManager.Application.Raport.Queries.GetProjectRaport;

    public class RaportController : BaseController
    {
        [HttpGet("{projectId}")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<string>> GetProjectRaport(int projectId)
        {
            return Ok(await base.Mediator.Send(new GetProjectRaportQuery { ProjectId = projectId }));
        }
    }
}
