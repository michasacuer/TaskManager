namespace TaskManager.Application.Interfaces
{
    using System.Threading.Tasks;
    using TaskManager.Domain.Entity;

    public interface ITokenService
    {
        Task<string> Generate(ApplicationUser user);
    }
}
