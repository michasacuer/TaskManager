namespace TaskManager.Application.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using TaskManager.Domain.Entity;
    using TaskManager.Domain.Enum;

    public class RegisterCommand : IRequest
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Role Role { get; set; }

        public class Handler : IRequestHandler<RegisterCommand>
        {
            private readonly UserManager<ApplicationUser> userManager;

            private readonly RoleManager<IdentityRole> roleManager;

            public Handler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
            {
                this.userManager = userManager;
                this.roleManager = roleManager;
            }

            public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                new RegisterCommandValidator().ValidateAndThrow(request);

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

                return Unit.Value;
            }
        }
    }
}
