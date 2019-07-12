namespace TaskManager.Application.Project.Commands.DeleteProject
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using TaskManager.Application.Interfaces;
    using TaskManager.Common.Exceptions;

    public class DeleteProjectCommand : IRequest
    {
        public int ProjectId { get; set; }

        public class Handler : IRequestHandler<DeleteProjectCommand>
        {
            private readonly ITaskManagerDbContext context;

            public Handler(ITaskManagerDbContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
            {
                var project = await this.context.Projects.FindAsync(request.ProjectId)
                    ?? throw new EntityNotFoundException();

                this.context.Projects.Remove(project);
                await this.context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
