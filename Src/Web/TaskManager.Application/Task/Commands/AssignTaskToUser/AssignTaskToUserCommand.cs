namespace TaskManager.Application.Task.Commands.AssignTaskToUser
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using TaskManager.Application.Interfaces;

    public class AssignTaskToUserCommand : IRequest
    {
        public int TaskId { get; set; }

        public string ApplicationUserId { get; set; }

        public class Handler : IRequestHandler<AssignTaskToUserCommand>
        {
            private readonly ITaskManagerDbContext context;

            public Handler(ITaskManagerDbContext context)
            {
                this.context = context;
            }

            public Task<Unit> Handle(AssignTaskToUserCommand request, CancellationToken cancellationToken)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
