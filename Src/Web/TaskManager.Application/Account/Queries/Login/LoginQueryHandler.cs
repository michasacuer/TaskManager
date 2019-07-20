namespace TaskManager.Application.Queries
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using TaskManager.Application.Interfaces;

    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginModel>
    {
        private readonly IApplicationUserRepository applicationUserRepository;

        public LoginQueryHandler(IApplicationUserRepository applicationUserRepository)
        {
            this.applicationUserRepository = applicationUserRepository;
        }

        public async Task<LoginModel> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            return await this.applicationUserRepository.LoginAsync(request.UserName, request.Password);
        }
    }
}
