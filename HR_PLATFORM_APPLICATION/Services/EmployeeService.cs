﻿using HR_PLATFORM_APPLICATION.Interface;
using HR_PLATFORM_APPLICATION.Model.CV;
using HR_PLATFORM_APPLICATION.Model.Employee;
using HR_PLATFORM_APPLICATION.Model.Vacation;
using HR_PLATFORM_DOMAIN.Entity.Employee;
using HR_PLATFORM_DOMAIN.Entity.Vacation;
using HR_PLATFORM_DOMAIN.Interface;
using HR_PLATFORM_INFRASTRUCTURE.Repositories;

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
                employeeModel.PhoneNumber,
                employeeModel.Department,
                employeeModel.Function,
                employeeModel.ContractCode,
                employeeModel.ContractDate,
                employeeModel.Studied,
                employeeModel.OperatorHR,
                employeeModel.codeManager,
                employeeModel.StatutEmployee,
                employeeModel.Grafic);

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
                PhoneNumber = employee.PhoneNumber,
                Department = employee.Department,
                Function = employee.Function,
                ContractCode = employee.ContractCode,
                ContractDate = employee.ContractDate,
                OperatorHR = employee.OperatorHR,
                Studied = employee.Studied,
                Grafic = employee.Grafic,
                StatutEmployee = employee.StatutEmployee
            };
        }

        public async Task<List<EmployeeModel>> GetEmployees()
        {
            var result = await _employeeRepository.GetEmployees();
            var employeesModels = result.Select(v => new EmployeeModel
            {
                FirstName = v.FirstName,
                LastName = v.LastName,
                Birthday = v.Birthday,
                Address = v.Address,
                Email = v.Email,
                PhoneNumber = v.PhoneNumber,
                Department = v.Department,
                Function = v.Function,
                ContractCode = v.ContractCode,
                ContractDate = v.ContractDate,
                OperatorHR = v.OperatorHR,
                Studied = v.Studied,
                codeManager = v.CodeManager,
                Grafic = v.Grafic,
                StatutEmployee = v.StatutEmployee
            }).ToList();

            return employeesModels;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            return await _employeeRepository.DeleteEmployeeAsync(id);
        }
        public async Task<bool> UpdateEmployeeAsync(int id, EmployeeModelUpdate updateEmployeeDto)
        {
            var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (existingEmployee == null)
            {
                return false;
            }
            if (updateEmployeeDto.FirstName != null && !updateEmployeeDto.FirstName.Equals("string"))
            {
                existingEmployee.FirstName = updateEmployeeDto.FirstName;
            }
            if (updateEmployeeDto.Address != null && !updateEmployeeDto.Address.Equals("string"))
            {
                existingEmployee.Address = updateEmployeeDto.Address;
            }
            if (updateEmployeeDto.Email != null && !updateEmployeeDto.Email.Equals("string"))
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
            if (updateEmployeeDto.Department != null && !updateEmployeeDto.Department.Equals("string"))
            {
                existingEmployee.Department = updateEmployeeDto.Department;
            }
            if (updateEmployeeDto.Function != null && !updateEmployeeDto.Function.Equals("string"))
            {
                existingEmployee.Function= updateEmployeeDto.Function;
            }
            if (updateEmployeeDto.ContractCode != 0)
            {
                existingEmployee.ContractCode = updateEmployeeDto.ContractCode;
            }
            if (updateEmployeeDto.Studied != null && !updateEmployeeDto.Studied.Equals("string"))
            {
                existingEmployee.Studied = updateEmployeeDto.Studied;
            }
            if (updateEmployeeDto.StatutEmployee)
            {
                existingEmployee.StatutEmployee = updateEmployeeDto.StatutEmployee;
            }
            if (updateEmployeeDto.Grafic != null && !updateEmployeeDto.Grafic.Equals("string"))
            {
                existingEmployee.Grafic = updateEmployeeDto.Grafic;
            }
            return await _employeeRepository.UpdateEmployeeAsync(id,existingEmployee);
        }

        public async Task AddUserImage(string fileName, byte[] fileData, int codeEmployee, string contentType)
        {
            var chechImage = await _employeeRepository.GetUserImage(codeEmployee);
            if (chechImage != null) 
            {
                await _employeeRepository.UpdateEmployeeImage(fileName, fileData, codeEmployee, contentType);
            }
            else
            {
                await _employeeRepository.AddEmployeeImage(fileName, fileData, codeEmployee, contentType);
            }
        }

        public async Task<GetImage> GetImage(int codeEmployee)
        {
            var result = await _employeeRepository.GetUserImage(codeEmployee);
            if(result != null)
            {

            var image = new GetImage
            {
                CodEmployee = result.CodEmployee,
                CVData = result.FileData,
                FileName = result.FileName,
                ContentType = result.ContentType,
            };
            return image;
            }
            else
            {
                return null;
            }
        }
    }
}
