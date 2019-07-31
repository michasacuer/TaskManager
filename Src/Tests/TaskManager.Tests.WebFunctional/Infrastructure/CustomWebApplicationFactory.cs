namespace TaskManager.Tests.WebFunctional.Infrastructure
{
    using System;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using TaskManager.Application.Interfaces;
    using TaskManager.Domain.Entity;
    using TaskManager.Persistence;
    using TaskManager.Tests.Infrastructure;

    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseSolutionRelativeContentRoot(AppContext.BaseDirectory);

            builder.ConfigureServices(services =>
            {
                services.AddScoped<ITaskManagerDbContext, TaskManagerDbContext>();
                services.AddDbContext<TaskManagerDbContext>(o => o.UseInMemoryDatabase(Guid.NewGuid().ToString()));

                services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 6;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireDigit = false;
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<TaskManagerDbContext>()
                .AddDefaultTokenProviders();

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;

                    var context = scopedServices.GetRequiredService<ITaskManagerDbContext>();
                    var userManager = scopedServices.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = scopedServices.GetRequiredService<RoleManager<IdentityRole>>();

                    var concreteContext = (TaskManagerDbContext)context;
                    concreteContext.Database.EnsureCreated();

                    ContextDataSeeding.Run(concreteContext, roleManager, userManager);
                    ContextDataSeeding.AddRolesToUsers(concreteContext, roleManager, userManager);
                }
            });

            base.ConfigureWebHost(builder);
        }

        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder()
                .UseStartup<TStartup>();
        }
    }
}
