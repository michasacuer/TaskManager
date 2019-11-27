namespace TaskManager.Application.Task.Queries.GetUserTask
{
    using MediatR;
    using TaskManager.Domain.Entity;

    public class GetUserTaskQuery : IRequest<ToDoTask>
    {
        public string ApplicationUserId { get; set; }
    }
}
