namespace TaskManager.Application.Task.Queries.GetAllTasks
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using TaskManager.Application.Interfaces;
    using TaskManager.Domain.Entity;

    public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, TasksListModel>
    {
        private readonly IRepository<ToDoTask> repository;

        public GetAllTasksQueryHandler(IRepository<ToDoTask> repository)
        {
            this.repository = repository;
        }

        public async Task<TasksListModel> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            var tasks = await this.repository.GetAllAsync();

            return new TasksListModel
            {
                ToDoTasks = tasks
            };
        }
    }
}
