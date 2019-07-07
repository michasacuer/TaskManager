namespace TaskManager.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using TaskManager.Application.Commands;

    public class AccountController : BaseController
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}