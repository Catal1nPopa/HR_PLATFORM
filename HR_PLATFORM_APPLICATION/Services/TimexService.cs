using HR_PLATFORM_APPLICATION.Interface;
using HR_PLATFORM_APPLICATION.Model.Timex;
using HR_PLATFORM_DOMAIN.Entity.ReportingTimex;
using HR_PLATFORM_DOMAIN.Interface;

namespace HR_PLATFORM_APPLICATION.Services
{
    public class TimexService(ITimexRepository timexRepository) : IReportingTimex
    {
        private readonly ITimexRepository _timexRepository = timexRepository;
        public async Task AddReportingTimex(ReportTimexModel reportingTimex)
        {
            var timexData = new ReportingTime(
                reportingTimex.CodeEmployee,
                reportingTimex.TimeFirstEntry,
                reportingTimex.TimeLastExit,
                reportingTimex.LocationEntry,
                reportingTimex.LocationExit,
                reportingTimex.TimeOnWork);
            await _timexRepository.AddReportingTimex(timexData);
        }

        public async Task<List<ReportTimexModel>> GetAllReportingTimex()
        {
            var getData = await _timexRepository.GetAllReportingTimex();

            var list = getData.Select(v => new ReportTimexModel
            {
                CodeEmployee = v.CodeEmployee,
                TimeFirstEntry = v.TimeFirstEntry,
                TimeLastExit = v.TimeLastExit,
                LocationEntry = v.LocationEntry,
                LocationExit = v.LocationExit,
                TimeOnWork = v.TimeOnWork
            }).ToList(); 
            return list;
        }

        public async Task<List<ReportTimexModel>> GetReportingTimexByEmployee(int codeEmployee)
        {
            var getData = await _timexRepository.GetReportingTimexByEmployee(codeEmployee);

            var list = getData.Select(v => new ReportTimexModel
            {
                CodeEmployee = v.CodeEmployee,
                TimeFirstEntry = v.TimeFirstEntry,
                TimeLastExit = v.TimeLastExit,
                LocationEntry = v.LocationEntry,
                LocationExit = v.LocationExit,
                TimeOnWork = v.TimeOnWork
            }).ToList();
            return list;
        }

        public async Task UpdateReportingTimex(ReportTimexModel reportingTimex)
        {
            var lastEntry = await _timexRepository.GetLastUserEntry(reportingTimex.CodeEmployee);

            if(reportingTimex.TimeLastExit != null )
            {
                lastEntry.TimeLastExit = reportingTimex.TimeLastExit;
            }
            if(reportingTimex.LocationExit != null )
            {
                lastEntry.LocationExit = reportingTimex.LocationExit;
            }
            lastEntry.TimeOnWork = (lastEntry.TimeLastExit - lastEntry.TimeFirstEntry).ToString();

            await _timexRepository.UpdateReportingTimex(lastEntry);
        }
    }
}
