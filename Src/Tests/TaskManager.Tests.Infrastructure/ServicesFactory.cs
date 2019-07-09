namespace TaskManager.Tests.Infrastructure
{
    using System;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using TaskManager.Application.Interfaces;
    using TaskManager.Infrastructure.Implementations;
    using TaskManager.Persistence;

    public class ServicesFactory
    {
        public static ServiceCollection Create()
        {
            var services = new ServiceCollection();

            services.AddDbContext<TaskManagerDbContext>(o => o.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            services.AddIdentity<Domain.Entity.ApplicationUser, IdentityRole>(options =>
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
