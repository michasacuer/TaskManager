using Microsoft.AspNetCore.Identity;
using TaskManager.Domain.Entity;
using TaskManager.Persistence;
using TaskManager.Tests.Infrastructure;

namespace TaskManager.Tests.WebFunctional.TestData
{
    public class TestSeed : IBaseSeed
    {
        public void Run(ref TaskManagerDbContext context)
        {
            ContextDataSeeding.Run(ref context);
        }

        public void Run(
            ref TaskManagerDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            ContextDataSeeding.Run(ref context, roleManager, userManager);
            ContextDataSeeding.AddRolesToUsers(ref context, roleManager, userManager);
        }
    }
}