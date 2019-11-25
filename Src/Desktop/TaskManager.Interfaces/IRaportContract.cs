namespace TaskManager.Contracts.Interfaces
{
    using System.Threading.Tasks;
    
    public interface IRaportContract
    {
        Task<string> GetPdfAsync(int projectId);
    }
}
