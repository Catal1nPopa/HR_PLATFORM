using Dapper;
using HR_PLATFORM_DOMAIN.Entity.Employee;
using HR_PLATFORM_DOMAIN.Entity.Vacation;
using HR_PLATFORM_DOMAIN.Interface;
using HR_PLATFORM_INFRASTRUCTURE.DbContext;
using System.Data;

namespace HR_PLATFORM_INFRASTRUCTURE.Repositories
{
    public class EmployeeRepository(DapperContext dapperContext) : IEmployeeRepository
    {
        private readonly DapperContext _dapperContext = dapperContext;

        public async Task<bool> AddEmployee(Employee employee)
        {
            var query = "INSERT INTO Employees (FirstName, LastName, Birthday, Address, Email, PhoneNumber, Department," +
                "[Function], ContractCode, ContractDate, Studied, OperatorHR, StatutEmployee) " +
                "VALUES (@FirstName, @LastName, @Birthday, @Address, @Email, @PhoneNumber, @Departament, " +
                "@Function, @ContractCode, @ContractDate, @Studied, @OperatorHR, @StatutEmployee)";

            var parameters = new DynamicParameters();
            parameters.Add("FirstName", employee.FirstName, DbType.String);
            parameters.Add("LastName", employee.LastName, DbType.String);
            parameters.Add("Birthday", employee.Birthday, DbType.String);
            parameters.Add("Address", employee.Address, DbType.String);
            parameters.Add("Email", employee.Email, DbType.String);
            parameters.Add("PhoneNumber", employee.PhoneNumber, DbType.Decimal);
            parameters.Add("Departament", employee.Department, DbType.String);
            parameters.Add("Function", employee.Function, DbType.String);
            parameters.Add("ContractCode", employee.ContractCode, DbType.Decimal);
            parameters.Add("ContractDate", employee.ContractDate, DbType.DateTime);
            parameters.Add("Studied", employee.Studied, DbType.String);
            parameters.Add("OperatorHR", employee.OperatorHR, DbType.String);
            parameters.Add("StatutEmployee", employee.StatutEmployee, DbType.Boolean);

            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
                connection.Close();
                return true;
            }
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var query = "DELETE FROM Employees WHERE CodEmployee = @id";

            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { Id = id });
                connection.Close();
                return true;
            }
        }

        public async Task<Employee> GetEmployeeByIdAsync(int codAngajat)
        {
            var query = "SELECT FirstName,LastName,Birthday,[Address],Email,PhoneNumber,Department,[Function],ContractCode,ContractDate,Studied,OperatorHR,CodEmployee,StatutEmployee FROM Employees WHERE CodEmployee = @codAngajat";

            using (var connection = _dapperContext.CreateConnection())
            {
                var dom = await connection.QuerySingleOrDefaultAsync<Employee>(query, new { codAngajat });
                connection.Close();
                return dom;
            }
        }

        public async Task<List<Employee>> GetEmployees()
        {
            var query = "SELECT * FROM Employees";

            using ( var  connection = _dapperContext.CreateConnection())
            {
                var result = ( await connection.QueryAsync<Employee>(query)).ToList();
                connection.Close();
                return result;
            }
        }


        public async Task<bool> UpdateEmployeeAsync(int id, Employee employee)
        {
            var checkEmployee = await GetEmployeeByIdAsync(id);
            if(checkEmployee == null)
            {
                return false;
            }

            var query = "UPDATE Employees SET FirstName = @FirstName, Address = @Address, Email = @Email," +
                "PhoneNumber = @PhoneNumber, Department = @Departament, [Function] = @Function," +
                "Studied = @Studied, StatutEmployee = @StatutEmployee WHERE CodEmployee = @id";
            var parameters = new DynamicParameters();
            parameters.Add("FirstName", employee.FirstName, DbType.String);
            parameters.Add("Address", employee.Address, DbType.String);
            parameters.Add("Email", employee.Email, DbType.String);
            parameters.Add("PhoneNumber", employee.PhoneNumber, DbType.Decimal);
            parameters.Add("Departament", employee.Department, DbType.String);
            parameters.Add("Function", employee.Function, DbType.String);
            parameters.Add("Studied", employee.Studied, DbType.String);
            parameters.Add("StatutEmployee", employee.StatutEmployee, DbType.Boolean);
            parameters.Add("id", id, DbType.Int32);

            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
                connection.Close();
                return true;
            }
        }
    }
}
