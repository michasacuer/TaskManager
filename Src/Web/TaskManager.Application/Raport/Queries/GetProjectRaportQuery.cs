namespace TaskManager.Application.Raport.Queries
{
    using MediatR;

    public class GetProjectRaportQuery : IRequest<string>
    {
        public int ProjectId { get; set; }
    }
}
