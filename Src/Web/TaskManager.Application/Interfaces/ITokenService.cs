namespace TaskManager.Application.Interfaces
{
    using System.Threading.Tasks;

    public interface ITokenService
    {
        Task<object> Generate();
    }
}
