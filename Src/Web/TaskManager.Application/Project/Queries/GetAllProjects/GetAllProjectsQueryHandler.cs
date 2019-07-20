namespace TaskManager.Application.Project.Queries.GetAllProjects
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using TaskManager.Application.Interfaces;

    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, ProjectsListModel>
    {
        private readonly IProjectRepository projectRepository;

        public GetAllProjectsQueryHandler(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        public async Task<ProjectsListModel> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await this.projectRepository.GetAllProjectsWithTasksAsync();

            return new ProjectsListModel
            {
                Projects = projects
            };
        }
    }
}
