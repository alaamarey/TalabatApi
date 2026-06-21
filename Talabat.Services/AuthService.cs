using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contract;

namespace Talabat.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(ApplicationUser applicationUser, UserManager<ApplicationUser> userManager)
        {
            #region Payload
            // *** (2) Private Claims *** 
            var userRoles = await userManager.GetRolesAsync(applicationUser);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email , applicationUser.Email),
                new Claim(ClaimTypes.Name , applicationUser.DisplayName)
            };

            foreach (var role in userRoles)
                claims.Add(new Claim(ClaimTypes.Role, role));
            #endregion





            #region Signature / Secret Key
            var secrtKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
            #endregion

            var tokenObj = new JwtSecurityToken
                (
                    audience: _configuration["Jwt:AudienceValue"],
                    issuer: _configuration["Jwt:IssuerValue"],
                    expires: DateTime.UtcNow.AddDays(double.Parse(_configuration["Jwt:ExpireValue"]!)),
                    claims: claims,
                    signingCredentials: new SigningCredentials(secrtKey, SecurityAlgorithms.HmacSha256)
                );
            var token = new JwtSecurityTokenHandler().WriteToken(tokenObj);
            return token;
        }
    }
}
