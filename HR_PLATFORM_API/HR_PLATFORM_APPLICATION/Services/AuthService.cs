using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using HR_PLATFORM_APPLICATION.Interface;
using HR_PLATFORM_DOMAIN.Entity.Auth;
using HR_PLATFORM_DOMAIN.Interface;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

            var jwtHandler = new JwtSecurityTokenHandler();
            string getKey = _configuration.GetSection("Jwt").GetSection("Key").Value;
            //var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var key = Encoding.ASCII.GetBytes(getKey);
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = identity,
                Expires = DateTime.Now.AddMinutes(1),
                SigningCredentials = credentials
            };

            var token = jwtHandler.CreateToken(tokenDescriptor);
            return jwtHandler.WriteToken(token);
        }

        public async Task CreateUserAsync(string username, string password, string role)
        {
            var passwordHash = HashPasword(password, out var salt);
            var user = new User(username, passwordHash, role, salt);
            await _userRepository.AddUserAsync(user);
        }

        string HashPasword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(64); //keySize 
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                350000, //iterations
                HashAlgorithmName.SHA512, //hashAlgorithm
                64); //keySize 
            return Convert.ToHexString(hash);
        }
    }
}
