using HR_PLATFORM_DOMAIN.Entity.Employee;


namespace HR_PLATFORM_DOMAIN.Interface
{
    public interface IEmployeeRepository
    {
        Task<bool> AddEmployee(Employee employee);
        Task<Employee> GetEmployeeByIdAsync(int codAngajat);
        Task<bool> DeleteEmployeeAsync(int id);
        Task<bool> UpdateEmployeeAsync(int id,Employee employee);
        Task<List<Employee>> GetEmployees();
    }
}
