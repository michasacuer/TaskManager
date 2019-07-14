namespace TaskManager.Application.Project.Queries.GetProject
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using TaskManager.Application.Interfaces;
    using TaskManager.Common.Exceptions;

    public class GetProjectQueryHandler : IRequestHandler<GetProjectQuery, ProjectModel>
    {
        private readonly ITaskManagerDbContext context;

        public GetProjectQueryHandler(ITaskManagerDbContext context)
        {
            this.context = context;
        }

        public async Task<ProjectModel> Handle(GetProjectQuery request, CancellationToken cancellationToken)
        {
            var project = await this.context.Projects.Include(p => p.Tasks).FirstOrDefaultAsync(p => p.Id == request.ProjectId)
                ?? throw new EntityNotFoundException();

            return new ProjectModel
            {
                Project = project
            };
        }
    }
}
