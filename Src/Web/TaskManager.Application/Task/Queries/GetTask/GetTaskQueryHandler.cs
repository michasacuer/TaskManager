namespace TaskManager.Application.Task.Queries.GetTask
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using TaskManager.Application.Interfaces;
    using TaskManager.Common.Exceptions;
    using TaskManager.Domain.Entity;

    public class GetTaskQueryHandler : IRequestHandler<GetTaskQuery, ToDoTask>
    {
        private readonly ITaskRepository taskRepository;

        public GetTaskQueryHandler(ITaskRepository taskRepository)
        {
            this.taskRepository = taskRepository;
        }

        public async Task<ToDoTask> Handle(GetTaskQuery request, CancellationToken cancellationToken)
        {
            var task = await taskRepository.GetByIdAsync(request.TaskId) ?? throw new EntityNotFoundException();

            return task;
        }
    }
}
