using Dapper;
using HR_PLATFORM_DOMAIN.Entity.CV;
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

        public async Task AddEmployeeImage(string filename, byte[] fileData, int codeEmployee, string contentType)
        {
            var query = "INSERT INTO USER_IMAGE (CodEmployee, FileName, FileData, UploadDate, ContentType) VALUES (@codeEmployee, @FileName, @FileData, SYSDATETIME(), @ContentType)";

            var parameters = new DynamicParameters();
            parameters.Add("codeEmployee", codeEmployee, DbType.Int32);
            parameters.Add("FileName", filename, DbType.String);
            parameters.Add("FileData", fileData, DbType.Binary);
            parameters.Add("ContentType", contentType, DbType.String);

            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
                connection.Close();
            }
        }

        public async Task UpdateEmployeeImage(string filename, byte[] fileData, int codeEmployee, string contentType)
        {
            var query = "UPDATE USER_IMAGE SET FileName = @FileName, FileData = @FileData, UploadDate = SYSDATETIME(), ContentType = @ContentType WHERE CodEmployee = @codeEmployee";

            var parameters = new DynamicParameters();
            parameters.Add("codeEmployee", codeEmployee, DbType.Int32);
            parameters.Add("FileName", filename, DbType.String);
            parameters.Add("FileData", fileData, DbType.Binary);
            parameters.Add("ContentType", contentType, DbType.String);

            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
                connection.Close();
            }
        }

        public async Task<Image> GetUserImage(int codeEmployee)
        {
            var query = "SELECT * FROM USER_IMAGE WHERE CodEmployee = @codeEmployee";

            var parameters = new DynamicParameters();
            parameters.Add("codeEmployee", codeEmployee, DbType.Int64);

            using (var connection = _dapperContext.CreateConnection())
            {
                var employeeCV = await connection.QueryFirstOrDefaultAsync<Image>(query, new { codeEmployee });
                connection.Close();
                return employeeCV;
            }
        }

        public async Task<bool> AddEmployee(Employee employee)
        {
            var query = "INSERT INTO Employees (FirstName, LastName, Birthday, Address, Email, PhoneNumber, Department," +
                "[Function], ContractCode, ContractDate, Studied, OperatorHR, CodeManager, StatutEmployee, Grafic) " +
                "VALUES (@FirstName, @LastName, @Birthday, @Address, @Email, @PhoneNumber, @Departament, " +
                "@Function, @ContractCode, @ContractDate, @Studied, @OperatorHR, @CodeManager, @StatutEmployee, @Grafic)";

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
            parameters.Add("CodeManager", employee.CodeManager, DbType.Int64);
            parameters.Add("StatutEmployee", employee.StatutEmployee, DbType.Boolean);
            parameters.Add("Grafic", employee.Grafic, DbType.String);

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
            var query = "SELECT FirstName,LastName,Birthday,[Address],Email,PhoneNumber,Department,[Function],ContractCode,ContractDate,Studied,OperatorHR,CodEmployee,StatutEmployee,Grafic FROM Employees WHERE CodEmployee = @codAngajat";

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
                "Studied = @Studied, StatutEmployee = @StatutEmployee, Grafic = @Grafic WHERE CodEmployee = @id";
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
            parameters.Add("Grafic", employee.Grafic, DbType.String);

            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
                connection.Close();
                return true;
            }
        }
    }
}
