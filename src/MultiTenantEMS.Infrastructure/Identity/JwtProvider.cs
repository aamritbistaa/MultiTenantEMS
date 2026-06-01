using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MultiTenantEMS.Application.Abstractions.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MultiTenantEMS.Infrastructure.Identity
{
    internal class JwtProvider : IJwtProvider
    {
        private readonly IConfiguration _configuration;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly string _secret;
        public JwtProvider(IConfiguration configuration)
        {
            _configuration = configuration;
            _issuer = _configuration["Jwt:Issuer"];
            _audience = _configuration["Jwt:Audience"];
            _secret = _configuration["Jwt:Secret"];
        }
        public string GenerateToken(string userId, string email, string role, string? tenantId)
        {

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, userId),
                new(ClaimTypes.Email, email),
                new(ClaimTypes.Role, role),
            };

            if (!string.IsNullOrWhiteSpace(tenantId))
            {
                claims.Add(new Claim("tenantId", tenantId));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));

            var credentials =new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials);
            

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}
