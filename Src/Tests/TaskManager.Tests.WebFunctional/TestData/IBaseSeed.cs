namespace TaskManager.Tests.WebFunctional.TestData
{
    using Microsoft.AspNetCore.Identity;
    using TaskManager.Persistence;
    using TaskManager.Domain.Entity;
    
    public interface IBaseSeed
    {
        void Run(ref TaskManagerDbContext context);

        void Run(
            ref TaskManagerDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager);
    }
}