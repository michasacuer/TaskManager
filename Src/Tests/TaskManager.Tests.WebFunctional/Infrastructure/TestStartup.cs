namespace TaskManager.Tests.WebFunctional.Infrastructure
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Reflection;
    using System.Text;
    using MediatR;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using TaskManager.Application.Commands;
    using TaskManager.Application.Interfaces;
    using TaskManager.Infrastructure.Implementations;
    using TaskManager.Persistence;
    using TaskManager.Persistence.Repository;
    using TaskManager.Tests.Web.Infrastructure.Hubs;
    using TaskManager.Tests.Web.Infrastructure.Implementations;

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
            services.AddScoped<ITokenService, TokenTestService>();
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

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = "TESTTESTTESTTESTTESTTEST",
                    ValidAudience = "TESTTESTTESTTESTTESTTEST",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TESTTESTTESTTESTTESTTEST")),
                    ClockSkew = TimeSpan.Zero
                };
            });

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
