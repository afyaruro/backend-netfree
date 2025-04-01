using System.Text;
using Application.Jwt;
using Domain.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;

namespace Infrastructure.Extensions.JWT
{
    public static class ConfigJWT
    {
        public static IServiceCollection AddJWT(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<Jwt>(config.GetSection("JwtSettings"));
            services.AddSingleton<IJWT>(sp => sp.GetRequiredService<IOptions<Jwt>>().Value);
            services.AddSingleton<JWTService>();

            var jwtConfig = config.GetSection("JwtSettings").Get<Jwt>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig!.issuer,
                    ValidAudience = jwtConfig.audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.key))
                };
            });

            return services;
        }
    }
}
