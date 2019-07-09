namespace TaskManager.Application.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using TaskManager.Application.Interfaces;

    public class CreateProjectCommand : IRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public class Handler : IRequestHandler<CreateProjectCommand>
        {
            private readonly ITaskManagerDbContext context;

            public Handler(ITaskManagerDbContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
