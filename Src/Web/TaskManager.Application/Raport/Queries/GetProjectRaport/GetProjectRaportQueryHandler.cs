namespace TaskManager.Application.Raport.Queries.GetProjectRaport
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using TaskManager.Application.Interfaces;

    public class GetProjectRaportQueryHandler : IRequestHandler<GetProjectRaportQuery, string>
    {
        private readonly IRaportService raportService;

        public GetProjectRaportQueryHandler(IRaportService raportService)
        {
            this.raportService = raportService;
        }

        public async Task<string> Handle(GetProjectRaportQuery request, CancellationToken cancellationToken)
        {
            return await this.raportService.GenerateProjectRaport(request.ProjectId);
        }
    }
}
