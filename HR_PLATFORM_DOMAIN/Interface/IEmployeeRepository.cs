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
        Task<Image> GetUserImage(int codeEmployee);
        Task UpdateEmployeeImage(string filename, byte[] fileData, int codeEmployee, string contentType);
        Task AddEmployeeImage(string filename, byte[] fileData, int codeEmployee, string contentType);

    }
}
