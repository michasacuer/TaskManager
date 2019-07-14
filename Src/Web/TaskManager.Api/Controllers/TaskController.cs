namespace TaskManager.Api.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using TaskManager.Application.Task.Commands.CreateTask;
    using TaskManager.Application.Task.Commands.DeleteTask;

    public class TaskController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody]CreateTaskCommand command)
        {
            return Ok(await base.Mediator.Send(command));
        }

        [HttpDelete("taskId")]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            return Ok(await base.Mediator.Send(new DeleteTaskCommand { TaskId = taskId }));
        }
    }
}