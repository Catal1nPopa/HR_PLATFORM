using HR_PLATFORM_DOMAIN.Entity.Vacation;

namespace HR_PLATFORM_DOMAIN.Interface
{
    public interface IVacationRepository
    {
        Task AddVacation(Vacation vacation);
        Task<bool> UpdateVacation(Vacation vacation);
        Task<Vacation> GetVacationByIdAsync(int idVacation);
        Task<List<Vacation>> GetAllVacations();
        Task<List<Vacation>> GetEmployeeVacations(int codeEmployee);
    }
}
