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
        /// <summary>
        /// Get all tasks
        /// </summary>
        /// <returns>All tasks</returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<TasksListModel>> GetAllTasks()
        {
            return Ok(await base.Mediator.Send(new GetAllTasksQuery()));
        }

        /// <summary>
        /// Get Task by ID
        /// </summary>
        /// <param name="taskId">Task ID</param>
        /// <returns>Concrete Task by ID</returns>
        [HttpGet("{taskId}")]
        [Authorize]
        public async Task<ActionResult<ToDoTask>> GetTask(int taskId)
        {
            return Ok(await base.Mediator.Send(new GetTaskQuery { TaskId = taskId }));
        }

        /// <summary>
        /// Create Task
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> CreateTask([FromBody]CreateTaskCommand command)
        {
            return Ok(await base.Mediator.Send(command));
        }

        /// <summary>
        /// Editing Task by ID
        /// </summary>
        /// <param name="command">Task ID</param>
        [HttpPost("Edit")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> EditTask([FromBody]EditTaskCommand command)
        {
            return Ok(await base.Mediator.Send(command));
        }

        /// <summary>
        /// Deleting Task by ID
        /// </summary>
        [HttpDelete("{taskId}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            return Ok(await base.Mediator.Send(new DeleteTaskCommand { TaskId = taskId }));
        }

        /// <summary>
        /// Take task by concrete user, status "in progress"
        /// </summary>
        [HttpPost("TakeTask")]
        [Authorize(Roles = "Developer, Manager")]
        public async Task<IActionResult> TakeTaskByUser([FromBody]TakeTaskByUserCommand command)
        {
            return Ok(await base.Mediator.Send(command));
        }

        /// <summary>
        /// When concrete user work on concrete task, it setting his status to "done"
        /// </summary>
        [HttpPost("EndTask")]
        [Authorize(Roles = "Developer, Manager")]
        public async Task<IActionResult> EndTaskByUser([FromBody]EndTaskByUserCommand command)
        {
            return Ok(await base.Mediator.Send(command));
        }

        /// <summary>
        /// Get task that user work on in this time
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>Concrete task based on User ID</returns>
        [HttpGet("UsersTask/{userId}")]
        [Authorize]
        public async Task<ActionResult<ToDoTask>> GetUserTask(string userId)
        {
            return Ok(await base.Mediator.Send(new GetUserTaskQuery { ApplicationUserId = userId} ));
        }
    }
}