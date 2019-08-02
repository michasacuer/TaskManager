namespace TaskManager.Tests.Web.Infrastructure.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.IdentityModel.Tokens;
    using TaskManager.Application.Interfaces;
    using TaskManager.Domain.Entity;

    public class TokenTestService : ITokenService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public TokenTestService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<string> Generate(ApplicationUser user)
        {
            var userRole = await this.userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Role, userRole[0])
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TESTTESTTESTTESTTESTTEST"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(30));

            var token = new JwtSecurityToken(
                "TESTTESTTESTTESTTESTTEST",
                "TESTTESTTESTTESTTESTTEST",
                claims,
                expires: expires,
                signingCredentials: creds);

            var bearer = new JwtSecurityTokenHandler().WriteToken(token);

            return (string)bearer;
        }
    }
}
