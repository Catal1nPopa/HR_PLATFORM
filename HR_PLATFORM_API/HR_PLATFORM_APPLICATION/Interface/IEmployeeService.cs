
using HR_PLATFORM_APPLICATION.Model.Employee;

namespace HR_PLATFORM_APPLICATION.Interface
{
    public interface IEmployeeService
    {
        Task AddEmployee(EmployeeModel employeeModel);
        Task<EmployeeModel> GetEmployeeByIdAsync(int codAngajat);
        Task<bool> DeleteEmployeeAsync(int id);
        Task<bool> UpdateEmployeeAsync(int id, EmployeeModel employee);
    }
}
