namespace TaskManager.Api.Middleware
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using TaskManager.Domain.Entity;
    
    // for fun, nothing special here
    public class CurrentUserMiddleware
    {
        private readonly RequestDelegate next;
        
        private readonly IConfiguration configuration;

        private readonly UserManager<ApplicationUser> userManager;

        public CurrentUserMiddleware(
            RequestDelegate next,
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager)
        {
            this.next = next;
            this.configuration = configuration;
            this.userManager = userManager;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var token = httpContext.Request.Headers["Authentication"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["JwtKey"]));
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = false,
                ValidateAudience = false
            };
            var claims = handler.ValidateToken(token, validations, out var tokenSecure);
        }
    }
}