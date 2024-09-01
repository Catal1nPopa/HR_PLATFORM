using Dapper;
using HR_PLATFORM_DOMAIN.Entity.Auth;
using HR_PLATFORM_DOMAIN.Entity.Vacation;
using HR_PLATFORM_DOMAIN.Interface;
using HR_PLATFORM_INFRASTRUCTURE.DbContext;
using System;
using System.Data;

namespace HR_PLATFORM_INFRASTRUCTURE.Repositories
{
    public class VacationRepository(DapperContext dapperContext) : IVacationRepository
    {
        private readonly DapperContext _dapperContext = dapperContext;
        public async Task AddVacation(Vacation vacation)
        {
            var query = "INSERT INTO Vacations(TypeVacation, VacationDaysLeft, DaysVacation, CodEmployee, EndDate, StartDate, CodeManager, Status)" +
                "VALUES(@TypeVacation, @VacationDaysLeft, @DaysVacation, @CodEmployee,@EndDate, @StartDate, @CodeManager, @Status)";

            var parameters = new DynamicParameters();
            parameters.Add("TypeVacation", vacation.TypeVacation, DbType.Int64);
            parameters.Add("VacationDaysLeft", vacation.VacationDaysLeft, DbType.Int64);
            parameters.Add("DaysVacation", vacation.DaysVacation, DbType.Int64);
            parameters.Add("CodEmployee", vacation.CodEmployee, DbType.Int64);
            parameters.Add("EndDate", vacation.EndDate, DbType.DateTime);
            parameters.Add("StartDate", vacation.StartDate, DbType.DateTime);
            parameters.Add("CodeManager", vacation.CodeManager, DbType.Int64);
            parameters.Add("Status", vacation.Status, DbType.String);

            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
                connection.Close();
            }
        }

        public async Task<List<Vacation>> GetAllVacations()
        {
            var query = "SELECT Id, TypeVacation, VacationDaysLeft, DaysVacation, CodEmployee, EndDate, StartDate FROM Vacations";

            using (var connection = _dapperContext.CreateConnection())
            {
                var result = (await connection.QueryAsync<Vacation>(query)).ToList();
                connection.Close();
                return result;
            }
        }

        public async Task<List<Vacation>> GetEmployeeVacations(int codeEmployee)
        {
            var query = "SELECT Id, TypeVacation, VacationDaysLeft, DaysVacation, CodEmployee, EndDate, StartDate FROM Vacations " +
                "WHERE CodEmployee = @CodEmployee";

            var parameters = new DynamicParameters();
            parameters.Add("CodEmployee", codeEmployee, DbType.Int64);

            using (var connection = _dapperContext.CreateConnection())
            {
                var result = (await connection.QueryAsync<Vacation>(query, parameters)).ToList();
                connection.Close();
                return result;
            }
        }

        public async Task<Vacation> GetVacationByIdAsync(int codVacation)
        {
            var query = "SELECT Id, TypeVacation, VacationDaysLeft, DaysVacation, CodEmployee, EndDate, StartDate FROM Vacations WHERE Id = @codVacation";
            using (var connection = _dapperContext.CreateConnection())
            {
                var result = ( await connection.QuerySingleOrDefaultAsync<Vacation>(query, new { codVacation }));
                connection.Close();
                return result;
            }
        }

        public async Task<bool> UpdateVacation(Vacation vacation)
        {
            var query = "UPDATE Vacations SET TypeVacation = @TypeVacation, VacationDaysLeft = @VacationDaysLeft, DaysVacation = @DaysVacation," +
                "EndDate = @EndDate, StartDate = @StartDate WHERE CodEmployee = @CodEmployee AND Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("TypeVacation", vacation.TypeVacation, DbType.String);
            parameters.Add("VacationDaysLeft", vacation.VacationDaysLeft, DbType.Int64);
            parameters.Add("DaysVacation", vacation.DaysVacation, DbType.Int64);
            parameters.Add("CodEmployee", vacation.CodEmployee, DbType.Int64);
            parameters.Add("EndDate", vacation.EndDate, DbType.DateTime);
            parameters.Add("StartDate", vacation.StartDate, DbType.DateTime);
            parameters.Add("Id", vacation.Id, DbType.Int64);

            using (var connection = _dapperContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
                connection.Close();
                return true;
            }
        }
    }
}
