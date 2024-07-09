using HR_PLATFORM_DOMAIN.Entity.Vacation;

namespace HR_PLATFORM_DOMAIN.Interface
{
    public interface IVacationRepository
    {
        Task AddVacation(Vacation vacation);
        Task<bool> UpdateVacation(int codEmployee, Vacation vacation);
        Task<Vacation> GetVacationByIdAsync(int codEmployee);
        Task<List<Vacation>> GetAllVacationsByEmployee(int codeEmployee);
    }
}
