using HR_PLATFORM_DOMAIN.Entity.ReportingTimex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLATFORM_DOMAIN.Interface
{
    public interface ITimexRepository
    {
        Task AddReportingTimex(ReportingTime reportingTimex);
        Task UpdateReportingTimex(ReportingTime reportingTimex);
        Task<List<ReportingTime>> GetAllReportingTimex();
        Task<List<ReportingTime>> GetReportingTimexByEmployee(int codeEmployee);
        Task<ReportingTime> GetLastUserEntry(int codeEmployee); 
    }
}
