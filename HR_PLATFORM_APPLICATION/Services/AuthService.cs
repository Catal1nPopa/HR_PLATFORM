using HR_PLATFORM_APPLICATION.Interface;
using HR_PLATFORM_APPLICATION.Model.Auth;
using HR_PLATFORM_APPLICATION.Model.Vacation;
using HR_PLATFORM_DOMAIN.Entity.Auth;
using HR_PLATFORM_DOMAIN.Interface;
using HR_PLATFORM_INFRASTRUCTURE.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HR_PLATFORM_APPLICATION.Services
{
    public class AuthService(IUserRepository userRepository, IConfiguration configuration, IEmployeeRepository employeeRepository)
        : IAuthService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IConfiguration _configuration = configuration;
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;

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
                new Claim(ClaimTypes.NameIdentifier, user.CodeEmployee)
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = identity,
                Expires = DateTime.Now.AddMinutes(30),
                SigningCredentials = credentials
            };
            var token = jwtHandler.CreateToken(tokenDescriptor);
            return new AuthResult
            {
                Token = jwtHandler.WriteToken(token),
                IsFirstLogin = user.FirstLogin,
            };
        }

        public async Task<bool> ChangeUserPassword(string username, string password, string codeEmployee)
        {
            var passwordHash = HashPasword(password, out var salt);
            var user = new User(username, passwordHash, "none", salt, false, codeEmployee);
            return  await _userRepository.UpdateUserPass(user);
        }

        public async Task<bool> CreateUserAsync(string username, string password, string role, int codeEmployee)
        {
            var checkEmployee = await _employeeRepository.GetEmployeeByIdAsync(codeEmployee);
            if (checkEmployee == null)
            {
                return false;
            }
            var passwordHash = HashPasword(password, out var salt);
            var user = new User(username, passwordHash, role, salt, true, codeEmployee.ToString());
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

        public async Task<List<UsersModel>> GetUsers()
        {
            var users = await _userRepository.GetUsers();

            var usersModels = users.Select(v => new UsersModel
            {
                username = v.Username,
                codeEmployee = v.CodeEmployee,
                firstLogin = v.FirstLogin,
            }).ToList();

            return usersModels;
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

        public async Task DeleteUserLogin(string username)
        {
            await _userRepository.DeleteUserLogin(username);
        }
    }
}
