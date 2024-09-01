using HR_PLATFORM_DOMAIN.Entity.Salary;

namespace HR_PLATFORM_DOMAIN.Interface
{
    public interface ISalaryRepository
    {
        Task AddEmployeeSalary(SalaryEmployee salary);
        Task<List<SalaryEmployee>> GetEmployeesSalary();
        Task<SalaryEmployee> GetEmployeeSalary(int codeEmployee);
        Task DeleteEmployeeSalary(int codeEmployee, DateTime dateTime);
        Task<List<SalaryHistory>> GetSalaryHistory(int codeEmployee, DateTime startDate, DateTime endDate);
        Task<List<SalaryHistory>> GetSalaryHistoryAll(DateTime startDate, DateTime endDate);
    }
}
