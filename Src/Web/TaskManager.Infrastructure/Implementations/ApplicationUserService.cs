namespace TaskManager.Infrastructure.Implementations
{
    using System.Threading.Tasks;
    using TaskManager.Application.Commands;
    using TaskManager.Application.Interfaces;
    using TaskManager.Application.Queries;

    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IApplicationUserRepository applicationUserRepository;

        public ApplicationUserService(IApplicationUserRepository applicationUserRepository)
        {
            this.applicationUserRepository = applicationUserRepository;
        }

        public async Task<LoginModel> LoginAsync(string requestUserName, string requestPassword)
        {
            return await this.applicationUserRepository.LoginAsync(requestUserName, requestPassword);
        }

        public async Task RegisterAsync(RegisterCommand request)
        {
            await this.applicationUserRepository.RegisterAsync(request);
        }
    }
}
