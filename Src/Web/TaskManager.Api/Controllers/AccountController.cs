namespace TaskManager.Api.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using TaskManager.Api.Models;
    using TaskManager.Application.Commands;
    using TaskManager.Application.Queries;

    public class AccountController : BaseController
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterCommand command)
        {
            return Ok(await base.Mediator.Send(command));
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginModel>> Login([FromBody]LoginViewModel model)
        {
            return Ok(await base.Mediator.Send(new LoginQuery
            {
                UserName = model.UserName,
                Password = model.Password
            }));
        }
    }
}