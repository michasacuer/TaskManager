namespace TaskManager.Application.Project.Queries.GetAllProjects
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using TaskManager.Application.Interfaces;

    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, ProjectsListViewModel>
    {
        private readonly ITaskManagerDbContext context;

        public GetAllProjectsQueryHandler(ITaskManagerDbContext context)
        {
            this.context = context;
        }

        public async Task<ProjectsListViewModel> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await this.context.Projects.ToListAsync();

            return new ProjectsListViewModel
            {
                Projects = projects
            };
        }
    }
}
