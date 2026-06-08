using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AbitElit.BusinessLogic
{
internal class AuthService(IConfiguration configuration) : IAuthService
    {
        public string? Authenticate(LoginRequest request)
        {
            string role = "";
            if (request.Username == "admin" && request.Password == "admin1111") role = "Admin";
            else if (request.Username == "reader" && request.Password == "reader1111") role = "Reader";
            else return null; 

            // Генерація токена
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, request.Username),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}