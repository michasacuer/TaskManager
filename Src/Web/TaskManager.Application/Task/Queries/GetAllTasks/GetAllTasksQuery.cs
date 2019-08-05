namespace TaskManager.Application.Task.Queries.GetAllTasks
{
    using MediatR;

    public class GetAllTasksQuery : IRequest<TasksListModel>
    {
    }
}
