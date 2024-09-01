

using HR_PLATFORM_APPLICATION.Model.Timex;

namespace HR_PLATFORM_APPLICATION.Interface
{
    public interface IReportingTimex
    {
        Task AddReportingTimex(ReportTimexModel reportingTimex);
        Task UpdateReportingTimex(ReportTimexModel reportingTimex);
        Task<List<ReportTimexModel>> GetAllReportingTimex();
        Task<List<ReportTimexModel>> GetReportingTimexByEmployee(int codeEmployee);
    }
}
