namespace TaskManager.Application.Project.Queries.GetProject
{
    using MediatR;

    public class GetProjectQuery : IRequest<ProjectModel>
    {
        public int ProjectId { get; set; }
    }
}
