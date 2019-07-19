namespace TaskManager.Application.Interfaces
{
    using System.Threading.Tasks;
    using TaskManager.Application.Commands;
    using TaskManager.Application.Queries;
    using TaskManager.Domain.Entity;

    public interface IApplicationUserRepository
    {
        Task RegisterAsync(RegisterCommand request);

        Task<LoginModel> LoginAsync(string requestUserName, string requestPassword);

        Task<ApplicationUser> GetByIdAsync(string id);

        Task<bool> UserInRoleAsync(ApplicationUser user, string roleName);
    }
}
