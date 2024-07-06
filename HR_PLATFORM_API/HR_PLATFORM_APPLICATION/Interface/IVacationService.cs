using HR_PLATFORM_APPLICATION.Model.Vacation;

namespace HR_PLATFORM_APPLICATION.Interface
{
    public interface IVacationService
    {
        Task AddVacationAsync(VacationModel vacationModel);
        Task<bool> UpdateVacationAsync(int codeEmployee, VacationModel vacationModel);
        Task<List<VacationModel>> GetVacationsByEmployee(int codeEmployee);
    }
}
