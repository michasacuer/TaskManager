namespace TaskManager.Persistence.Repository
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using TaskManager.Application.Commands;
    using TaskManager.Application.Interfaces;
    using TaskManager.Application.Queries;
    using TaskManager.Common.Exceptions;
    using TaskManager.Domain.Entity;

    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly UserManager<ApplicationUser> userManager;

        private readonly RoleManager<IdentityRole> roleManager;

        private readonly SignInManager<ApplicationUser> signInManager;

        private readonly ITokenService tokenService;

        public ApplicationUserRepository(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenService tokenService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
        }

        public async Task<ApplicationUser> GetByIdAsync(string id)
        {
            var user = await this.userManager.FindByIdAsync(id) ?? throw new UserNotFoundException();

            return user;
        }

        public async Task<LoginModel> LoginAsync(string requestUserName, string requestPassword)
        {
            var result = await this.signInManager
                .PasswordSignInAsync(requestUserName, requestPassword, false, false);

            if (result.Succeeded)
            {
                var appUser = this.userManager.Users.SingleOrDefault(r => r.UserName == requestUserName);
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

        public async Task RegisterAsync(RegisterCommand request)
        {
            var user = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            await this.userManager.CreateAsync(user, request.Password);

            var roleName = request.Role.ToString();
            if (!await this.roleManager.RoleExistsAsync(roleName))
            {
                var role = new IdentityRole(roleName);
                await this.roleManager.CreateAsync(role);
            }

            await this.userManager.UpdateSecurityStampAsync(user);
            await this.userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<bool> UserInRoleAsync(ApplicationUser user, string roleName)
        {
            return await this.userManager.IsInRoleAsync(user, roleName);
        }
    }
}
