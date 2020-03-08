namespace TaskManager.Application.Task.Queries.GetAllEndedTasks
{
    using MediatR;
    
    public class GetAllEndedTasksQuery : IRequest<EndedTasksModel>
    {
    }
}
