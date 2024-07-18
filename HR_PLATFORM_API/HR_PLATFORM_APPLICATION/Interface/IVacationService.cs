using HR_PLATFORM_APPLICATION.Model.Vacation;

namespace HR_PLATFORM_APPLICATION.Interface
{
    public interface IVacationService
    {
        Task AddVacationAsync(VacationModel vacationModel);
        Task<bool> UpdateVacationAsync(VacationModel vacationModel);
        Task<List<VacationModel>> GetVacationsEmployees();
        Task<List<VacationModel>> GetEmployeeVacations(int employeeId);
    }
}
