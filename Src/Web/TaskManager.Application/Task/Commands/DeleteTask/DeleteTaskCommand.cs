namespace TaskManager.Application.Task.Commands.DeleteTask
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using TaskManager.Application.Interfaces;
    using TaskManager.Common.Exceptions;

    public class DeleteTaskCommand : IRequest
    {
        public int TaskId { get; set; }

        public class Handler : IRequestHandler<DeleteTaskCommand>
        {
            private readonly ITaskManagerDbContext context;

            public Handler(ITaskManagerDbContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
            {
                var task = await this.context.Tasks.FindAsync(request.TaskId)
                    ?? throw new EntityNotFoundException();

                this.context.Tasks.Remove(task);
                await this.context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
