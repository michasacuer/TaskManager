namespace TaskManager.Application.Task.Commands.EndTaskByUser
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using TaskManager.Application.Interfaces;

    public class EndTaskByUserCommand : IRequest
    {
        public string ApplicationUserId { get; set; }

        public int TaskId { get; set; }

        public class Handler : IRequestHandler<EndTaskByUserCommand>
        {
            private readonly ITaskManagerDbContext context;

            public Handler(ITaskManagerDbContext context)
            {
                this.context = context;
            }

            public Task<Unit> Handle(EndTaskByUserCommand request, CancellationToken cancellationToken)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
