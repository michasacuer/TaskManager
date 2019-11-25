namespace TaskManager.Api.Extensions
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Reflection;
    using System.Text;
    using MediatR;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using TaskManager.Api.Filters;
    using TaskManager.Application.Commands;
    using TaskManager.Application.Interfaces;
    using TaskManager.Domain.Entity;
    using TaskManager.Infrastructure.Implementations;
    using TaskManager.Persistence;
    using TaskManager.Persistence.Repository;

    public static class StartupExtension
    {
        public static void InjectServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(options =>
                options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddSingleton(configuration);

            services.AddMediatR(typeof(RegisterCommand.Handler).GetTypeInfo().Assembly);
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IRaportService, RaportService>();
            services.AddScoped<IApplicationUserService, ApplicationUserService>();
            services.AddHttpContextAccessor();
            services.AddSignalR();

            services.AddScoped<ITaskManagerDbContext, TaskManagerDbContext>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();

            services.AddDbContext<TaskManagerDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("TaskManagerDatabase")));

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
                    ValidIssuer = configuration["JwtIssuer"],
                    ValidAudience = configuration["JwtIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}
