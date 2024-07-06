using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HR_PLATFORM_APPLICATION.Interface;
using HR_PLATFORM_DOMAIN.Entity.Auth;
using HR_PLATFORM_DOMAIN.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HR_PLATFORM_APPLICATION.Services
{
    public class AuthService(IUserRepository userRepository, IConfiguration configuration)
        : IAuthService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IConfiguration _configuration = configuration;

        public async Task<string> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsername(username);
            if (user == null || !user.CheckPassword(password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Username) }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task CreateUserAsync(string username, string password)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new User(username, passwordHash);
            await _userRepository.AddUserAsync(user);
        }
    }
}
