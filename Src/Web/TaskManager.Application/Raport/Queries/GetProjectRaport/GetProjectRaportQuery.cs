namespace TaskManager.Application.Raport.Queries.GetProjectRaport
{
    using MediatR;

    public class GetProjectRaportQuery : IRequest<string>
    {
        public int ProjectId { get; set; }
    }
}
