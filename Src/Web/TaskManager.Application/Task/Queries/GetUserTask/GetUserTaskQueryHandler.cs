namespace TaskManager.Application.Task.Queries.GetUserTask
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using TaskManager.Application.Interfaces;
    using TaskManager.Common.Exceptions;

    public class GetUserTaskQueryHandler : IRequestHandler<GetUserTaskQuery, TaskModel>
    {
        private readonly ITaskRepository taskRepository;

        public GetUserTaskQueryHandler(ITaskRepository taskRepository)
        {
            this.taskRepository = taskRepository;
        }

        public async Task<TaskModel> Handle(GetUserTaskQuery request, CancellationToken cancellationToken)
        {
            if (request.ApplicationUserId == null)
            {
                throw new EntityNotFoundException();
            }

            var userTask = await this.taskRepository.GetUserTask(request.ApplicationUserId)
                ?? throw new EntityNotFoundException();

            return new TaskModel
            {
                Task = userTask
            };
        }
    }
}
