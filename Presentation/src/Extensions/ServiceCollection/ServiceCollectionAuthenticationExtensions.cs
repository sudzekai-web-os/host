using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Presentation.src.Types.Exceptions;
using System.Text;

namespace Presentation.src.Extensions.ServiceCollection
{
    internal static class ServiceCollectionAuthenticationExtensions
    {
        public static IServiceCollection ConfigureAuthentication(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            string secret = configuration["Jwt:Secret"] ?? throw new ConfigurationVariableNotFoundException("Jwt:Secret");
            string issuer = configuration["Jwt:Issuer"] ?? throw new ConfigurationVariableNotFoundException("Jwt:Issuer");
            string audience = configuration["Jwt:Audience"] ?? throw new ConfigurationVariableNotFoundException("Jwt:Audience");

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                    ClockSkew = TimeSpan.FromMinutes(1)
                };
            });

            services.AddAuthorization();

            return services;
        }
    }
}
