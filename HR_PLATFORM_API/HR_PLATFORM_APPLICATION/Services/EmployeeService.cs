﻿using HR_PLATFORM_APPLICATION.Interface;
using HR_PLATFORM_APPLICATION.Model.Employee;
using HR_PLATFORM_DOMAIN.Entity.Employee;
using HR_PLATFORM_DOMAIN.Interface;

namespace HR_PLATFORM_APPLICATION.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task AddEmployee(EmployeeModel employeeModel)
        {
            var employee = new Employee(
                employeeModel.FirstName,
                employeeModel.LastName,
                employeeModel.Birthday,
                employeeModel.Address,
                employeeModel.Email,
                employeeModel.CodEmployee,
                employeeModel.PhoneNumber,
                employeeModel.Department,
                employeeModel.Function,
                employeeModel.Salary,
                employeeModel.ContractDate,
                employeeModel.Studied,
                employeeModel.OperatorHR,
                employeeModel.StatutEmployee);

            await _employeeRepository.AddEmployee(employee);
        }

        public async Task<EmployeeModel> GetEmployeeByIdAsync(int codAngajat)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(codAngajat);
            if (employee == null)
            {
                return null;
            }

            return new EmployeeModel
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Birthday = employee.Birthday,
                Address = employee.Address,
                Email = employee.Email,
                CodEmployee = employee.CodEmployee,
                PhoneNumber = employee.PhoneNumber,
                Department = employee.Department,
                Function = employee.Function,
                Salary = employee.Salary,
                ContractDate = employee.ContractDate,
                OperatorHR = employee.OperatorHR,
                StatutEmployee = employee.StatutEmployee
            };
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            return await _employeeRepository.DeleteEmployeeAsync(id);
        }
        public async Task<bool> UpdateEmployeeAsync(int id, EmployeeModel updateEmployeeDto)
        {
            var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (existingEmployee == null)
            {
                return false;
            }
            if (updateEmployeeDto.FirstName != null)
            {
                existingEmployee.FirstName = updateEmployeeDto.FirstName;
            }
            if (updateEmployeeDto.Address != null)
            {
                existingEmployee.Address = updateEmployeeDto.Address;
            }
            if (updateEmployeeDto.Email != null)
            {
                existingEmployee.Email = updateEmployeeDto.Email;
            }
            if (updateEmployeeDto.CodEmployee != 0)
            {
                existingEmployee.CodEmployee = updateEmployeeDto.CodEmployee;
            }
            if (updateEmployeeDto.PhoneNumber != 0)
            {
                existingEmployee.PhoneNumber = updateEmployeeDto.PhoneNumber;
            }
            if (updateEmployeeDto.Department != null)
            {
                existingEmployee.Department = updateEmployeeDto.Department;
            }
            if (updateEmployeeDto.Function != null)
            {
                existingEmployee.Function= updateEmployeeDto.Function;
            }
            if (updateEmployeeDto.Salary != 0)
            {
                existingEmployee.Salary = updateEmployeeDto.Salary;
            }
            if (updateEmployeeDto.Studied != null)
            {
                existingEmployee.Studied = updateEmployeeDto.Studied;
            }
            if (updateEmployeeDto.StatutEmployee)
            {
                existingEmployee.StatutEmployee = updateEmployeeDto.StatutEmployee;
            }
            return await _employeeRepository.UpdateEmployeeAsync(id,existingEmployee);
        }

    }
}
