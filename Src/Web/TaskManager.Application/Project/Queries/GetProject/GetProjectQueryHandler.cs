namespace TaskManager.Application.Project.Queries.GetProject
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using TaskManager.Application.Interfaces;

    public class GetProjectQueryHandler : IRequestHandler<GetProjectQuery, ProjectModel>
    {
        private readonly ITaskManagerDbContext context;

        public GetProjectQueryHandler(ITaskManagerDbContext context)
        {
            this.context = context;
        }

        public Task<ProjectModel> Handle(GetProjectQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
