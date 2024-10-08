﻿
using HR_PLATFORM_APPLICATION.Model.Employee;

namespace HR_PLATFORM_APPLICATION.Interface
{
    public interface IEmployeeService
    {
        Task AddEmployee(EmployeeModel employeeModel);
        Task<EmployeeModel> GetEmployeeByIdAsync(int codAngajat);
        Task<bool> DeleteEmployeeAsync(int id);
        Task<bool> UpdateEmployeeAsync(int id, EmployeeModelUpdate employee);
        Task<List<EmployeeModel>> GetEmployees();
        Task AddUserImage(string fileName, byte[] fileData, int codeEmployee, string contentType);
        Task<GetImage> GetImage(int codeEmployee);
    }
}
