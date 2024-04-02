using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace UsersMinimalAPI.Authentication
{
    public class JWTTokenGenerator : IJWTTokenGenerator
    {
        private readonly IConfiguration _configuration;

        public JWTTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(string username)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,username)
            };
            var signingKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Token")));
            var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(claims:claims,expires:DateTime.UtcNow.AddHours(2),signingCredentials:creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
