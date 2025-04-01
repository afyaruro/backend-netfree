
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.JWT;
using Microsoft.IdentityModel.Tokens;

namespace Application.Jwt
{
    public class JWTService
    {
        private readonly IJWT _jwtConfig;

        public JWTService(IJWT jwtConfig)
        {
            _jwtConfig = jwtConfig;
        }

        public string generateToken(string userId)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _jwtConfig.issuer,
                audience: _jwtConfig.audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtConfig.expiryMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}