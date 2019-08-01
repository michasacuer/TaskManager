namespace TaskManager.Application.Task.Queries.GetUserTask
{
    using MediatR;

    public class GetUserTaskQuery : IRequest<TaskModel>
    {
        public string ApplicationUserId { get; set; }
    }
}
