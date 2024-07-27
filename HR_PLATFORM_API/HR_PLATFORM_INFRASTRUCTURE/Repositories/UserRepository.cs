using Dapper;
using HR_PLATFORM_DOMAIN.Entity.Auth;
using HR_PLATFORM_INFRASTRUCTURE.DbContext;
using HR_PLATFORM_DOMAIN.Interface;
using System.Data;
using HR_PLATFORM_DOMAIN.Entity.Vacation;

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
            var query = "INSERT INTO Users (Username, CodeEmployee, PasswordHash, Role, Salt, FirstLogin) " +
                "VALUES (@Username, @CodeEmployee, @PasswordHash, @Role, @Salt, @FirstLogin)";

            var parameters = new DynamicParameters();
            parameters.Add("Username", user.Username, DbType.String);
            parameters.Add("CodeEmployee", user.CodeEmployee, DbType.String);
            parameters.Add("PasswordHash", user.PasswordHash, DbType.String);
            parameters.Add("Role", user.Role, DbType.String);
            parameters.Add("Salt", user.Salt, DbType.Binary);
            parameters.Add("FirstLogin", user.FirstLogin, DbType.Boolean);
            
            using(var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
                connection.Close();
                return true;
            }
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var query = "SELECT * FROM Users WHERE USERNAME = @username";

            using ( var connection = _dapperContext.CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<User>(query, new {  username });
                connection.Close();
                return result;
            }
        }

        public async Task<List<User>> GetUsers()
        {
            var query = "SELECT * FROM Users";

            using ( var connection = _dapperContext.CreateConnection() )
            {
                var result = (await connection.QueryAsync<User>(query)).ToList();
                connection.Close();
                return result;
            }
        }

        public async Task<bool> UpdateUserPass(User user)
        {
            var checkUser = await GetUserByUsername(user.Username);
            if(checkUser == null)
            {
                return false;
            }
            var query = "UPDATE Users SET PasswordHash = @PasswordHash, Salt = @Salt, FirstLogin = @FirstLogin WHERE Username = @Username AND CodeEmployee = @CodeEmployee";
            var statusLogin = false;
            var parameters = new DynamicParameters();
            parameters.Add("Username", user.Username, DbType.String);
            parameters.Add("CodeEmployee", user.CodeEmployee, DbType.String);
            parameters.Add("PasswordHash", user.PasswordHash, DbType.String);
            parameters.Add("Salt", user.Salt, DbType.Binary);
            parameters.Add("FirstLogin", statusLogin, DbType.Boolean);

            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
                connection.Close();
                return true;
            }
        }
    }
}
