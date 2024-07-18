using Dapper;
using HR_PLATFORM_DOMAIN.Entity.Auth;
using HR_PLATFORM_INFRASTRUCTURE.DbContext;
using HR_PLATFORM_DOMAIN.Interface;
using System.Data;

namespace HR_PLATFORM_INFRASTRUCTURE.Repositories
{
    public class UserRepository(DapperContext dapperContext) : IUserRepository
    {
        private const bool V = false;
        private readonly DapperContext _dapperContext = dapperContext;

        public async Task<bool> AddUserAsync(User user)
        {
            var checkUser = await GetUserByUsername(user.Username);
            if (checkUser != null)
            {
                return V;
            }
            var query = "INSERT INTO Users (Username, PasswordHash,Role, Salt, FirstLogin) " +
                "VALUES (@Username, @PasswordHash, @Role, @Salt, @FirstLogin)";

            var parameters = new DynamicParameters();
            parameters.Add("Username", user.Username, DbType.String);
            parameters.Add("PasswordHash", user.PasswordHash, DbType.String);
            parameters.Add("Role", user.Role, DbType.String);
            parameters.Add("Salt", user.Salt, DbType.Binary);
            parameters.Add("FirstLogin", user.FirstLogin, DbType.Boolean);
            
            using(var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
                return true;
            }
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var query = "SELECT * FROM Users WHERE USERNAME = @username";

            using (var connection = _dapperContext.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<User>(query, new {  username });
            }
        }

        public async Task<bool> UpdateUserPass(User user)
        {
            var checkUser = await GetUserByUsername(user.Username);
            if(checkUser == null)
            {
                return false;
            }
            var query = "UPDATE Users SET PasswordHash = @PasswordHash, Salt = @Salt, FirstLogin = @FirstLogin WHERE Username = @Username";
            var statusLogin = false;
            var parameters = new DynamicParameters();
            parameters.Add("Username", user.Username, DbType.String);
            parameters.Add("PasswordHash", user.PasswordHash, DbType.String);
            parameters.Add("Salt", user.Salt, DbType.Binary);
            parameters.Add("FirstLogin", statusLogin, DbType.Boolean);

            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
                return true;
            }
        }
    }
}
