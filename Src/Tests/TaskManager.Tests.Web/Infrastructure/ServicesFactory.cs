namespace TaskManager.Tests.Infrastructure
{
    using System;
    using System.IO;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
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
            var signInManager = services.ServiceProvider.GetRequiredService<SignInManager<ApplicationUser>>();
            var tokenService = services.ServiceProvider.GetRequiredService<ITokenService>();

            ContextDataSeeding.Run(context, roleManager, userManager);
            ContextDataSeeding.AddRolesToUsers(context, roleManager, userManager);

            return new ServicesModel
            {
                Context = context,
                UserManager = userManager,
                RoleManager = roleManager,
                SignInManager = signInManager,
                TokenService = tokenService
            };
        }

        private static ServiceCollection CreateServiceCollection()
        {
            var services = new ServiceCollection();

            string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

            var basePath 
                = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("Tests")) + "Web/TaskManager.Api";

            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.Local.json", optional: true)
                .AddJsonFile($"appsettings.{AspNetCoreEnvironment}.json", optional: true)
                .AddEnvironmentVariables();

            IConfiguration configuration = builder.Build();

            services.AddScoped(c => configuration);
            services.AddScoped<ITokenService, TokenService>();
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

            return services;
        }
    }
}
