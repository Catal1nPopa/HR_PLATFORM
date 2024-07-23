using HR_PLATFORM_APPLICATION.Interface;
using HR_PLATFORM_APPLICATION.Model.Auth;
using HR_PLATFORM_DOMAIN.Entity.Auth;
using HR_PLATFORM_DOMAIN.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HR_PLATFORM_APPLICATION.Services
{
    public class AuthService(IUserRepository userRepository, IConfiguration configuration)
        : IAuthService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IConfiguration _configuration = configuration;

        public async Task<AuthResult> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsername(username);
            if (user == null || !user.CheckPassword(password))
            {
                return null;
            }
            if (user.FirstLogin)
            {
                return new AuthResult
                {
                    Token = null,
                    IsFirstLogin = user.FirstLogin,
                };
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
            return new AuthResult
            {
                Token = jwtHandler.WriteToken(token),
                IsFirstLogin = user.FirstLogin,
            };
        }

        public async Task<bool> ChangeUserPassword(string username, string password)
        {
            var passwordHash = HashPasword(password, out var salt);
            var user = new User(username, passwordHash, "none", salt, false);
            return  await _userRepository.UpdateUserPass(user);
        }

        public async Task<bool> CreateUserAsync(string username, string password, string role)
        {
            var passwordHash = HashPasword(password, out var salt);
            var user = new User(username, passwordHash, role, salt, true);
            return await _userRepository.AddUserAsync(user);
        }

        public async Task<NewUserModel> GetUser(string username, string password)
        {
            var user = await _userRepository.GetUserByUsername(username);
            if (user == null || !user.CheckPassword(password))
            {
                return null;
            }
            var userModel = new NewUserModel(
                user.Username,
                user.PasswordHash,
                user.FirstLogin
                );
            return userModel;
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
