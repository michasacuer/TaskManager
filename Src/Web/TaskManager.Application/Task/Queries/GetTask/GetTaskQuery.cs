namespace TaskManager.Application.Task.Queries.GetTask
{
    using MediatR;
    using TaskManager.Domain.Entity;

    public class GetTaskQuery : IRequest<ToDoTask>
    {
        public int TaskId { get; set; }
    }
}
