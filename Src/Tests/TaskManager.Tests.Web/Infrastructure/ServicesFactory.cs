namespace TaskManager.Tests.Infrastructure
{
    using System;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using TaskManager.Application.Interfaces;
    using TaskManager.Domain.Entity;
    using TaskManager.Infrastructure.Implementations;
    using TaskManager.Persistence;

    public class ServicesFactory
    {
        public static ServicesModel CreateProperServices()
        {
            var services = CreateServiceCollection().BuildServiceProvider().CreateScope();

            var context = services.ServiceProvider.GetRequiredService<TaskManagerDbContext>();
            var userManager = services.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            ContextDataSeeding.Run(context, roleManager, userManager);
            ContextDataSeeding.AddRolesToUsers(context, roleManager, userManager);

            return new ServicesModel
            {
                Context = context,
                UserManager = userManager,
                RoleManager = roleManager
            };
        }

        private static ServiceCollection CreateServiceCollection()
        {
            var services = new ServiceCollection();

            services.AddDbContext<TaskManagerDbContext>(o => o.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<TaskManagerDbContext>();

            services.AddTransient<ITokenService, TokenService>();

            return services;
        }
    }
}
