namespace TaskManager.Application.Task.Commands.DeleteTask
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using TaskManager.Application.Interfaces;
    using TaskManager.Common.Exceptions;
    using TaskManager.Domain.Entity;

    public class DeleteTaskCommand : IRequest
    {
        public int TaskId { get; set; }

        public class Handler : IRequestHandler<DeleteTaskCommand>
        {
            private readonly IRepository<ToDoTask> taskRepository;

            public Handler(IRepository<ToDoTask> taskRepository)
            {
                this.taskRepository = taskRepository;
            }

            public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
            {
                var task = await this.taskRepository.GetByIdAsync(request.TaskId)
                    ?? throw new EntityNotFoundException();

                this.taskRepository.Delete(task);
                await this.taskRepository.SaveAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
