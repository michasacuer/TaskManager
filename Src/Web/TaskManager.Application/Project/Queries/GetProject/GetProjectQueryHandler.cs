namespace TaskManager.Application.Project.Queries.GetProject
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using TaskManager.Application.Interfaces;
    using TaskManager.Common.Exceptions;

    public class GetProjectQueryHandler : IRequestHandler<GetProjectQuery, ProjectModel>
    {
        private readonly IProjectRepository projectRepository;

        public GetProjectQueryHandler(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        public async Task<ProjectModel> Handle(GetProjectQuery request, CancellationToken cancellationToken)
        {
            var project = await this.projectRepository.GetProjectWithTasksAsync(request.ProjectId)
                ?? throw new EntityNotFoundException();

            return new ProjectModel
            {
                Project = project
            };
        }
    }
}
