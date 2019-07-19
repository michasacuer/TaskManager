namespace TaskManager.Application.Interfaces
{
    using System.Threading.Tasks;
    using TaskManager.Application.Commands;
    using TaskManager.Application.Queries;

    public interface IApplicationUserRepository
    {
        Task Register(RegisterCommand request);

        Task<LoginModel> Login(string requestUserName, string requestPassword);
    }
}
