namespace TaskManager.Application.Project.Queries.GetAllProjects
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using TaskManager.Application.Interfaces;

    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, ProjectsListModel>
    {
        private readonly ITaskManagerDbContext context;

        public GetAllProjectsQueryHandler(ITaskManagerDbContext context)
        {
            this.context = context;
        }

        public async Task<ProjectsListModel> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await this.context.Projects.Include(t => t.Tasks).ToListAsync();

            return new ProjectsListModel
            {
                Projects = projects
            };
        }
    }
}
