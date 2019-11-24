namespace TaskManager.Application.EndedTask.Queries.GetAllEndedTasks
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using TaskManager.Application.Interfaces;
    using TaskManager.Domain.Entity;

    public class GetAllEndedTasksQueryHandler : IRequestHandler<GetAllEndedTasksQuery, EndedTasksModel>
    {
        private readonly IRepository<EndedTask> repository;

        public GetAllEndedTasksQueryHandler(IRepository<EndedTask> repository)
        {
            this.repository = repository;
        }

        public async Task<EndedTasksModel> Handle(GetAllEndedTasksQuery request, CancellationToken cancellationToken)
        {
            var endedTasks = await this.repository.GetAllAsync();

            return new EndedTasksModel
            {
                List = endedTasks
            };
        }
    }
}
