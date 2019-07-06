namespace TaskManager.Application
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using TaskManager.Application.Interfaces;
    using TaskManager.Domain.Entity;
    using TaskManager.Domain.Enum;

    public class RegisterUserCommand : IRequest
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Role Role { get; set; }

        public class Handler : IRequestHandler<RegisterUserCommand>
        {
            private readonly SignInManager<ApplicationUser> signInManager;

            private readonly UserManager<ApplicationUser> userManager;

            private readonly RoleManager<IdentityRole> roleManager;

            private readonly IConfiguration configuration;

            private readonly ITokenService tokenService;

            public Handler(
                SignInManager<ApplicationUser> signInManager,
                UserManager<ApplicationUser> userManager,
                RoleManager<IdentityRole> roleManager,
                IConfiguration configuration,
                ITokenService tokenService)
            {
                this.signInManager = signInManager;
                this.userManager = userManager;
                this.roleManager = roleManager;
                this.configuration = configuration;
                this.tokenService = tokenService;
            }

            public Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
