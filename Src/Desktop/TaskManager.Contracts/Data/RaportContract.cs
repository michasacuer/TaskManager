namespace TaskManager.Contracts.Data
{
    using System.Threading.Tasks;
    using TaskManager.Contracts.Extensions;
    using TaskManager.Contracts.Interfaces;

    public class RaportContract : BaseContract, IRaportContract
    {
        public RaportContract(string bearer) 
            : base(bearer)
        {
        }

        public async Task<string> GetPdfAsync(int projectId) => await base.httpClient.GetPdfAsync(projectId);
    }
}
