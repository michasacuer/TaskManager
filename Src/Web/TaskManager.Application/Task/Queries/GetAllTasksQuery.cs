namespace TaskManager.Application.Task.Queries
{
    using MediatR;

    public class GetAllTasksQuery : IRequest<TasksListModel>
    {
    }
}
