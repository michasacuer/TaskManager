namespace TaskManager.Api.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TaskManager.Application.Task.Commands.CreateTask;
    using TaskManager.Application.Task.Commands.DeleteTask;
    using TaskManager.Application.Task.Commands.EditTask;
    using TaskManager.Application.Task.Commands.EndTaskByUser;
    using TaskManager.Application.Task.Commands.TakeTaskByUser;
    using TaskManager.Application.Task.Queries.GetAllTasks;
    using TaskManager.Application.Task.Queries.GetTask;
    using TaskManager.Application.Task.Queries.GetUserTask;
    using TaskManager.Domain.Entity;

    public class TaskController : BaseController
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<TasksListModel>> GetAllTasks()
        {
            return Ok(await base.Mediator.Send(new GetAllTasksQuery()));
        }

        [HttpGet("{taskId}")]
        [Authorize]
        public async Task<ActionResult<ToDoTask>> GetTask(int taskId)
        {
            return Ok(await base.Mediator.Send(new GetTaskQuery { TaskId = taskId }));
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> CreateTask([FromBody]CreateTaskCommand command)
        {
            return Ok(await base.Mediator.Send(command));
        }

        [HttpPost("Edit")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> EditTask([FromBody]EditTaskCommand command)
        {
            return Ok(await base.Mediator.Send(command));
        }

        [HttpDelete("{taskId}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            return Ok(await base.Mediator.Send(new DeleteTaskCommand { TaskId = taskId }));
        }

        [HttpPost("TakeTask")]
        [Authorize(Roles = "Developer, Manager")]
        public async Task<IActionResult> TakeTaskByUser([FromBody]TakeTaskByUserCommand command)
        {
            return Ok(await base.Mediator.Send(command));
        }

        [HttpPost("EndTask")]
        [Authorize(Roles = "Developer, Manager")]
        public async Task<IActionResult> EndTaskByUser([FromBody]EndTaskByUserCommand command)
        {
            return Ok(await base.Mediator.Send(command));
        }

        [HttpPost("{userId}")]
        [Authorize]
        public async Task<ActionResult<TaskModel>> GetUserTask(string userId)
        {
            return Ok(await base.Mediator.Send(new GetUserTaskQuery { ApplicationUserId = userId} ));
        }
    }
}