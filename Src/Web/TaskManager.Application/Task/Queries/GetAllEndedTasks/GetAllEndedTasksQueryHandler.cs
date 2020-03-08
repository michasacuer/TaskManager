namespace TaskManager.Application.Task.Queries.GetAllEndedTasks
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using TaskManager.Application.Interfaces;
    
    public class GetAllEndedTasksQueryHandler : IRequestHandler<GetAllEndedTasksQuery, EndedTasksModel>
    {
        private readonly ITaskRepository taskRepository;

        public GetAllEndedTasksQueryHandler(ITaskRepository taskRepository)
        {
            this.taskRepository = taskRepository;
        }

        public async Task<EndedTasksModel> Handle(GetAllEndedTasksQuery request, CancellationToken cancellationToken)
        {
            var endedTasks = await this.taskRepository.GetAllEndedTasksAsync();

            return new EndedTasksModel
            {
                List = endedTasks
            };
        }
    }
}
