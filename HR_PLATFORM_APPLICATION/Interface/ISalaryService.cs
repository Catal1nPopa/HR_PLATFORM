using HR_PLATFORM_APPLICATION.Model.Salary;
using HR_PLATFORM_DOMAIN.Entity.Salary;

namespace HR_PLATFORM_APPLICATION.Interface
{
    public interface ISalaryService
    {
        Task AddEmployeeSalary(SalaryModel salary);
        Task<List<SalaryModel>> GetEmployeesSalary();
        Task<SalaryModel> GetEmployeeSalary(int codeEmployee);
        Task DeleteEmployeeSalary(int codeEmployee, DateTime dateTime);
        Task<List<SalaryHistory>> GetSalaryHistory(int codeEmployee, DateTime startDate, DateTime endDate);
        Task<List<SalaryHistory>> GetSalaryHistoryAll(DateTime startDate, DateTime endDate);
    }
}
