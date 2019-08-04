namespace TaskManager.Application.Interfaces
{
    using System.Threading.Tasks;
    using TaskManager.Application.Commands;
    using TaskManager.Application.Queries;

    public interface IApplicationUserService
    {
        Task<LoginModel> LoginAsync(string requestUserName, string requestPassword);

        Task RegisterAsync(RegisterCommand request);
    }
}
