using Dapper;
using HR_PLATFORM_DOMAIN.Entity.CV;
using HR_PLATFORM_DOMAIN.Entity.Employee;
using HR_PLATFORM_DOMAIN.Interface;
using HR_PLATFORM_INFRASTRUCTURE.DbContext;
using System.Data;

namespace HR_PLATFORM_INFRASTRUCTURE.Repositories
{
    public class CVRepository(DapperContext dapperContext) : ICVRepository
    {
        private readonly DapperContext _dapperContext = dapperContext;

        public async Task AddCV(string filename, byte[] fileData, int codeEmployee, string contentType)
        {
            var query = "INSERT INTO CV (CodEmployee, FileName, FileData, UploadDate, ContentType) VALUES (@codeEmployee, @FileName, @FileData, SYSDATETIME(), @ContentType)";

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

        public async Task UpdateCV(string filename, byte[] fileData, int codeEmployee, string contentType)
        {
            var query = "UPDATE CV SET FileName = @FileName, FileData = @FileData, UploadDate = SYSDATETIME(), ContentType = @ContentType WHERE CodEmployee = @codeEmployee";

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

        public async Task<CV> DownloadCV(int codeEmployee)
        {
            var query = "SELECT * FROM CV WHERE CodEmployee = @codeEmployee";

            var parameters = new DynamicParameters();
            parameters.Add("codeEmployee", codeEmployee, DbType.Int64);

            using ( var connection = _dapperContext.CreateConnection())
            {
                var employeeCV = await connection.QueryFirstOrDefaultAsync<CV>(query, new { codeEmployee});
                connection.Close();
                return employeeCV;
            }
        }
    }
}
