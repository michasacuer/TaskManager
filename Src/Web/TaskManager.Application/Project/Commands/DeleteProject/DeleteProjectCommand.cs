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
            private readonly IProjectRepository projectRepository;

            public Handler(IProjectRepository projectRepository)
            {
                this.projectRepository = projectRepository;
            }

            public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
            {
                var project = await this.projectRepository.GetByIdAsync(request.ProjectId)
                    ?? throw new EntityNotFoundException();

                this.projectRepository.Delete(project);
                await this.projectRepository.SaveAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
