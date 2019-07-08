namespace TaskManager.Application.Queries
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using TaskManager.Application.Exceptions;
    using TaskManager.Application.Interfaces;
    using TaskManager.Domain.Entity;

    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginModel>
    {
        private readonly SignInManager<ApplicationUser> signInManager;

        private readonly UserManager<ApplicationUser> userManager;

        private readonly ITokenService tokenService;

        public LoginQueryHandler(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ITokenService tokenService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.tokenService = tokenService;
        }

        public async Task<LoginModel> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var result = await this.signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = this.userManager.Users.SingleOrDefault(r => r.UserName == request.UserName);
                var appUserRoles = await this.userManager.GetRolesAsync(appUser);

                var userRole = appUserRoles.FirstOrDefault();
                var bearer = await this.tokenService.Generate(appUser);

                return new LoginModel
                {
                    Id = appUser.Id,
                    UserName = appUser.UserName,
                    FirstName = appUser.FirstName,
                    LastName = appUser.LastName,
                    Role = userRole,
                    Bearer = bearer
                };
            }
            else
            {
                throw new EntityNotFoundException();
            }
        }
    }
}
