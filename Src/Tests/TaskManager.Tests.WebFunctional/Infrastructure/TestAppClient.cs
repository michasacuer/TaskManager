using System;
using System.Net.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Domain.Entity;
using TaskManager.Persistence;
using TaskManager.Tests.Infrastructure;
using TaskManager.Tests.WebFunctional.TestData;

namespace TaskManager.Tests.WebFunctional.Infrastructure
{
    public class TestAppClient : IDisposable
    {
        public HttpClient Client { get; set; }

        public TestAppClient(IBaseSeed seed)
        {
            var factory = new CustomWebApplicationFactory<TestStartup>();
            this.Client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var sp = services.BuildServiceProvider();
                    using (var scope = sp.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;

                        var context = scopedServices.GetRequiredService<TaskManagerDbContext>();
                        var userManager = scopedServices.GetRequiredService<UserManager<ApplicationUser>>();
                        var roleManager = scopedServices.GetRequiredService<RoleManager<IdentityRole>>();

                        context.Database.EnsureCreated();

                        seed.Run(ref context);
                        ContextDataSeeding.AddRolesToUsers(ref context, roleManager, userManager);
                    }
                });
            }).CreateClient();
        }

        public void Dispose()
        {
            this.Client?.Dispose();
        }
    }
}