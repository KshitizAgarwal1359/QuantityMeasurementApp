using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BusinessLogic.API.Models;
namespace BusinessLogic.API.Security
{
    // UC18: JWT token generator using HMAC-SHA256.
    public class JwtTokenGenerator
    {
        private readonly IConfiguration configuration;
        public JwtTokenGenerator(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // Generate JWT token for authenticated user
        public string GenerateToken(UserEntity user)
        {
            string secretKey = configuration["Jwt:SecretKey"]!;
            string issuer = configuration["Jwt:Issuer"]!;
            string audience = configuration["Jwt:Audience"]!;
            int expirationMinutes = int.Parse(configuration["Jwt:ExpirationMinutes"]!);
            byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(keyBytes);
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Username));
            claims.Add(new Claim(ClaimTypes.Name, user.Username));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            DateTime expiration = DateTime.UtcNow.AddMinutes(expirationMinutes);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                null,
                expiration,
                credentials);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(token);
        }
        // Get token expiration time
        public DateTime GetExpirationTime()
        {
            int expirationMinutes = int.Parse(configuration["Jwt:ExpirationMinutes"]!);
            return DateTime.UtcNow.AddMinutes(expirationMinutes);
        }
    }
}
