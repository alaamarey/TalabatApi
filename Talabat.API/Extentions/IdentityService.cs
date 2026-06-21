using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contract;
using Talabat.repository.Identity;
using Talabat.Services;

namespace Talabat.API.Extentions
{
    public static class IdentityService
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection service, IConfiguration configuration)
        {


            service.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                option.Password.RequireLowercase = true;
            }).AddEntityFrameworkStores<TalabatIdentityDbContext>();

            service.AddScoped(typeof(IAuthService), typeof(AuthService));





            #region Authentication Handler

            //اللي باعنا خسر دلعنا 
            service.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                // “نعمل إيه لو اليوزر مش authenticated؟”  
                // غالبًا يرجّع 401 Unauthorized
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                }).AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateAudience = true,
                            ValidAudience = configuration["Jwt:AudienceValue"],

                            ValidateIssuer = true,
                            ValidIssuer = configuration["Jwt:IssuerValue"],

                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.FromDays(double.Parse(configuration["Jwt:ExpireValue"])),

                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]))
                        };
                    });
            #endregion


            return service;
        }


    }
}
