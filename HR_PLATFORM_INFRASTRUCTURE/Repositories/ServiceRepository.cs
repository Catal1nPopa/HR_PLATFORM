using Dapper;
using HR_PLATFORM_DOMAIN.Interface;
using HR_PLATFORM_INFRASTRUCTURE.DbContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLATFORM_INFRASTRUCTURE.Repositories
{
    public class ServiceRepository(DapperContext dapperContext) : IServiceRepository
    {
        private readonly DapperContext _dapperContext = dapperContext;
        public async Task<int> GetEmployeeManager(int codeEmployee)
        {
            var query = @"
            WITH RecursiveHierarchy AS (
                SELECT 
                    e.codEmployee, 
                    e.StatutEmployee, 
                    e.CodeManager
                FROM 
                    Employees e
                WHERE 
                    e.codEmployee = @CodEmployee

                UNION ALL

                SELECT 
                    m.codEmployee, 
                    m.StatutEmployee, 
                    m.CodeManager
                FROM 
                    Employees m
                INNER JOIN 
                    RecursiveHierarchy rh ON rh.CodeManager = m.codEmployee
            )
            SELECT TOP 1 codEmployee 
            FROM RecursiveHierarchy
            WHERE StatutEmployee = 1 
            ORDER BY CodeManager;
        ";

            var parameters = new DynamicParameters();
            parameters.Add("CodEmployee", codeEmployee, DbType.Int32);

            using (var connection = _dapperContext.CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<int>(query, parameters);
                return result;
            }
        }
    }
}
