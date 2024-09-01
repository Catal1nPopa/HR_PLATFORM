using Dapper;
using HR_PLATFORM_DOMAIN.Entity.Salary;
using HR_PLATFORM_DOMAIN.Interface;
using HR_PLATFORM_INFRASTRUCTURE.DbContext;
using System.Data;

namespace HR_PLATFORM_INFRASTRUCTURE.Repositories
{
    public class SalaryRepository(DapperContext dapperContext) : ISalaryRepository
    {
        public readonly DapperContext _dapperContext = dapperContext;
        public async Task AddEmployeeSalary(SalaryEmployee salary)
        {
            var query = @"INSERT INTO Salary (CodEmployee, Salary, SalaryPerDay, DateSet)
                        VALUES(@CodEmployee, @Salary, @SalaryPerDay, @DateSet)";

            var paramteres = new DynamicParameters();
            paramteres.Add("CodEmployee", salary.CodEmployee, DbType.Int64);
            paramteres.Add("Salary", salary.Salary, DbType.Decimal);
            paramteres.Add("SalaryPerDay", salary.SalaryPerDay, DbType.Decimal);
            paramteres.Add("DateSet", salary.DateTime, DbType.DateTime);

            using( var connection = _dapperContext.CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(query, paramteres);
                connection.Close();
            }
        }

        public async Task DeleteEmployeeSalary(int codeEmployee, DateTime dateTime)
        {
            var query = @"DELETE FROM Salary WHERE CodEmployee = @CodEmployee and DateSet = @DataTime";

            var paramteres = new DynamicParameters();
            paramteres.Add("CodEmployee", codeEmployee, DbType.Int64);
            paramteres.Add("DataTime", dateTime, DbType.DateTime);

            using ( var connection = _dapperContext.CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(query);
                connection.Close();
            }
        }

        public async Task<SalaryEmployee> GetEmployeeSalary(int codeEmployee)
        {
            var query = @"SELECT CodEmployee, Salary, SalaryPerDay, DateSet FROM Salary WHERE CodEmployee = @CodEmployee";

            using ( var connection = _dapperContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<SalaryEmployee>(query, new { CodEmployee = codeEmployee });
                connection.Close();
                return result;
            }
        }

        public async Task<List<SalaryEmployee>> GetEmployeesSalary()
        {
            var query = @"SELECT CodEmployee, Salary, SalaryPerDay, DateSet FROM Salary";

            using (var connection = _dapperContext.CreateConnection())
            {
                var result = (await connection.QueryAsync<SalaryEmployee>(query)).ToList();
                connection.Close();
                return result;
            }
        }

        public async Task<List<SalaryHistory>> GetSalaryHistory(int codeEmployee, DateTime startDate, DateTime endDate)
        {
            var query = @"SELECT CodeEmployee, Salary_Brut, Salary_Net, Data_Send 
                  FROM Salary_History 
                  WHERE CodeEmployee = @CodeEmployee
                  AND Data_Send BETWEEN @StartDate AND @EndDate";

            using (var connection = _dapperContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<SalaryHistory>(query, new
                {
                    CodeEmployee = codeEmployee,
                    StartDate = startDate,
                    EndDate = endDate
                });
                connection.Close();
                return result.ToList();
            }
        }


        public async Task<List<SalaryHistory>> GetSalaryHistoryAll(DateTime startDate, DateTime endDate)
        {
            var query = @"SELECT CodeEmployee, Salary_Brut, Salary_Net, Data_Send FROM Salary_History WHERE Data_Send between @StartDate and @EndDate";
            
            using (var connection = _dapperContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<SalaryHistory>(query, new
                {
                    StartDate = startDate,
                    EndDate = endDate
                });
                connection.Close();
                return result.ToList();
            }
        }
    }
}
