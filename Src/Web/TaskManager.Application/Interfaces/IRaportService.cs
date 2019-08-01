namespace TaskManager.Application.Interfaces
{
    using System.Threading.Tasks;

    public interface IRaportService
    {
        Task<string> GenerateProjectRaport(int projectId);
    }
}
