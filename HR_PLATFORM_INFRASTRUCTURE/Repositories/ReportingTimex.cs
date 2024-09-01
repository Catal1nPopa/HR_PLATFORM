using Dapper;
using HR_PLATFORM_DOMAIN.Entity.ReportingTimex;
using HR_PLATFORM_DOMAIN.Interface;
using HR_PLATFORM_INFRASTRUCTURE.DbContext;

namespace HR_PLATFORM_INFRASTRUCTURE.Repositories
{
    public class ReportingTimex(DapperContext dapperContext) : ITimexRepository
    {
        private readonly DapperContext _dapperContext = dapperContext;
        public async Task AddReportingTimex(ReportingTime reportingTimex)
        {
            var query = @"INSERT INTO ReportingTime (CodeEmployee, TimeFirstEntry, TimeLastExit, LocationEntry, LocationExit, TimeOnWork)
                  VALUES (@CodeEmployee, @TimeFirstEntry, @TimeLastExit, @LocationEntry, @LocationExit, @TimeOnWork)";

            using (var connection = _dapperContext.CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(query, new
                {
                    reportingTimex.CodeEmployee,
                    reportingTimex.TimeFirstEntry,
                    reportingTimex.TimeLastExit,
                    reportingTimex.LocationEntry,
                    reportingTimex.LocationExit,
                    reportingTimex.TimeOnWork
                });
                connection.Close();
            }
        }

        public async Task<List<ReportingTime>> GetAllReportingTimex()
        {
            var query = "SELECT * FROM ReportingTime";

            using (var connection = _dapperContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<ReportingTime>(query);
                connection.Close();
                return result.ToList();
            }
        }

        public async Task<List<ReportingTime>> GetReportingTimexByEmployee(int codeEmployee)
        {
            var query = "SELECT * FROM ReportingTime WHERE CodeEmployee = @CodeEmployee";

            using (var connection = _dapperContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryAsync<ReportingTime>(query, new { CodeEmployee = codeEmployee });
                connection.Close();
                return result.ToList();
            }
        }


        public async Task UpdateReportingTimex(ReportingTime reportingTimex)
        {
            var query = @"UPDATE ReportingTime 
                  SET  TimeLastExit = @TimeLastExit,
                      LocationExit = @LocationExit, 
                      TimeOnWork = @TimeOnWork 
                  WHERE CodeEmployee = @CodeEmployee AND TimeFirstEntry = @TimeFirstEntry";

            using (var connection = _dapperContext.CreateConnection())
            {
                connection.Open();
                await connection.ExecuteAsync(query, new
                {
                    reportingTimex.CodeEmployee,
                    reportingTimex.TimeFirstEntry,
                    reportingTimex.TimeLastExit,
                    reportingTimex.LocationExit,
                    reportingTimex.TimeOnWork
                });
                connection.Close();
            }
        }

        public async Task<ReportingTime> GetLastUserEntry(int codeEmployee)
        {
            var query = "SELECT * FROM ReportingTime WHERE CodeEmployee = @CodeEmployee ORDER BY TimeFirstEntry DESC";

            using (var connection = _dapperContext.CreateConnection())
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<ReportingTime>(query, new { codeEmployee });
                connection.Close();
                return result;
            }
        }
    }
}
