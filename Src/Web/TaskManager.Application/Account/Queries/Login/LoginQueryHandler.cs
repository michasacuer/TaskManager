namespace TaskManager.Application.Queries
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using TaskManager.Application.Interfaces;

    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginModel>
    {
        private readonly IApplicationUserService applicationUserService;

        public LoginQueryHandler(IApplicationUserService applicationUserService)
        {
            this.applicationUserService = applicationUserService;
        }

        public async Task<LoginModel> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            return await this.applicationUserService.LoginAsync(request.UserName, request.Password);
        }
    }
}
