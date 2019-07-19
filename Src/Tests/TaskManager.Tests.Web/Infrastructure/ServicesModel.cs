namespace TaskManager.Tests.Infrastructure
{
    using Microsoft.AspNetCore.Identity;
    using TaskManager.Application.Interfaces;
    using TaskManager.Domain.Entity;
    using TaskManager.Persistence;

    public class ServicesModel
    {
        public TaskManagerDbContext Context { get; set; }

        public UserManager<ApplicationUser> UserManager { get; set; }

        public RoleManager<IdentityRole> RoleManager { get; set; }

        public SignInManager<ApplicationUser> SignInManager { get; set; }

        public ITokenService TokenService { get; set; }
    }
}
