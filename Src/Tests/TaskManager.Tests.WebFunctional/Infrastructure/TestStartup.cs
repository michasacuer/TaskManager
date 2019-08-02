namespace TaskManager.Tests.WebFunctional.Infrastructure
{
    using MediatR;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System.Reflection;
    using TaskManager.Application.Commands;
    using TaskManager.Application.Interfaces;
    using TaskManager.Infrastructure.Implementations;
    using TaskManager.Persistence;
    using TaskManager.Persistence.Repository;
    using TaskManager.Tests.Web.Infrastructure.Hubs;

    public class TestStartup
    {
        public TestStartup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options => 
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
            

            services.AddMediatR(typeof(RegisterCommand.Handler).GetTypeInfo().Assembly);
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IRaportService, RaportService>();
            services.AddHttpContextAccessor();
            services.AddSignalR();

            services.AddDbContext<TaskManagerDbContext>(options =>
                options.UseInMemoryDatabase("TEST"));

            services.AddScoped<ITaskManagerDbContext, TaskManagerDbContext>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();
            app.UseHttpsRedirection();
            app.UseSignalR(routes => routes.MapHub<NotificationTestHub>("/Notifications"));
        }
    }
}
