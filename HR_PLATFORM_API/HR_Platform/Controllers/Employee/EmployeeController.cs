using HR_PLATFORM.DTOs.Employee;
using HR_PLATFORM_APPLICATION.Interface;
using HR_PLATFORM_APPLICATION.Model.Employee;
using HR_PLATFORM_DOMAIN.Entity.Employee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_PLATFORM.Controllers.Employee
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddEmployee([FromBody] AddEmployeeDto addEmployeeDto)
        {
            try
            {
                var employeeModel = new EmployeeModel
                {
                    FirstName = addEmployeeDto.FirstName,
                    LastName = addEmployeeDto.LastName,
                    Birthday = addEmployeeDto.Birthday,
                    Address = addEmployeeDto.Address,
                    Email = addEmployeeDto.Email,
                    CodEmployee = addEmployeeDto.CodEmployee,
                    PhoneNumber = addEmployeeDto.PhoneNumber,
                    Department = addEmployeeDto.Department,
                    Function = addEmployeeDto.Function,
                    Salary = addEmployeeDto.Salary,
                    ContractDate = addEmployeeDto.ContractDate,
                    Studied = addEmployeeDto.Studied,
                    OperatorHR = addEmployeeDto.OperatorHR,
                    StatutEmployee = addEmployeeDto.StatutEmployee
                };

                await _employeeService.AddEmployee(employeeModel);
                return Ok(new { message = "Adaugare angajat nou cu succes", status = "success" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message, status = "error" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            var employeeDto = new EmployeeDto
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
                Studied = employee.Studied,
                OperatorHR = employee.OperatorHR,
                StatutEmployee = employee.StatutEmployee,
            };

            return Ok(new { employeeDto, status = "success" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _employeeService.DeleteEmployeeAsync(id);
            if (result)
            {
                return Ok( new { status = "succes" });
            }
            return NotFound();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeDto employee)
        {
            var employeeDto = new EmployeeModel
            {
                FirstName = employee.FirstName,
                Address = employee.Address,
                Email = employee.Email,
                CodEmployee = employee.CodEmployee,
                PhoneNumber = employee.PhoneNumber,
                Department = employee.Department,
                Function = employee.Function,
                Salary = employee.Salary,
                Studied = employee.Studied,
                StatutEmployee = employee.StatutEmployee,
            };
            var result = await _employeeService.UpdateEmployeeAsync(id, employeeDto);
            if (result)
            {
                return Ok();
            }
            return NotFound();
        }

    }
}
