namespace TaskManager.Infrastructure.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using TaskManager.Application.Interfaces;
    using TaskManager.Domain.Entity;

    public class TokenService : ITokenService
    {
        private readonly string userName;

        private readonly ApplicationUser user;

        private readonly IConfiguration configuration;

        private readonly UserManager<ApplicationUser> userManager;

        public TokenService(string userName, ApplicationUser user, IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            this.userName = userName;
            this.user = user;
            this.configuration = configuration;
            this.userManager = userManager;
        }

        public async Task<object> Generate()
        {
            var userRole = await this.userManager.GetRolesAsync(this.user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, this.userName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, this.user.Id),
                new Claim(ClaimTypes.Role, userRole[0])
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(this.configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                this.configuration["JwtIssuer"],
                this.configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
