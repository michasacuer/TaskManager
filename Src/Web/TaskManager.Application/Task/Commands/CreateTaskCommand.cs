namespace TaskManager.Application.Task.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using TaskManager.Application.Interfaces;
    using TaskManager.Domain.Enum;

    public class CreateTaskCommand : IRequest
    {
        public int ProjectId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Priority Priority { get; set; }

        public int? StoryPoints { get; set; }

        public class Handler : IRequestHandler<CreateTaskCommand>
        {
            private readonly ITaskManagerDbContext context;

            public Handler(ITaskManagerDbContext context)
            {
                this.context = context;
            }

            public Task<Unit> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
