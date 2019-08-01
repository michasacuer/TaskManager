namespace TaskManager.Api
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Reflection;
    using System.Text;
    using MediatR;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using TaskManager.Application.Commands;
    using TaskManager.Application.Interfaces;
    using TaskManager.Domain.Entity;
    using TaskManager.Infrastructure.Hubs;
    using TaskManager.Infrastructure.Implementations;
    using TaskManager.Persistence;
    using TaskManager.Persistence.Repository;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSingleton(Configuration);

            services.AddMediatR(typeof(RegisterCommand.Handler).GetTypeInfo().Assembly);
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IRaportService, RaportService>();
            services.AddHttpContextAccessor();
            services.AddSignalR();

            services.AddScoped<ITaskManagerDbContext, TaskManagerDbContext>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();

            services.AddDbContext<TaskManagerDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("TaskManagerDatabase")));

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
                    ValidIssuer = this.Configuration["JwtIssuer"],
                    ValidAudience = this.Configuration["JwtIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["JwtKey"])),
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
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyHeader().AllowAnyOrigin());
            app.UseHttpsRedirection();
            app.UseSignalR(routes => routes.MapHub<NotificationHub>("/Notifications"));
        }
    }
}
