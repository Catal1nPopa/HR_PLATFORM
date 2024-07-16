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
    public class EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger) : ControllerBase
    {
        private readonly IEmployeeService _employeeService = employeeService;
        private readonly ILogger<EmployeeController> _logger = logger;

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
                    ContractCode = addEmployeeDto.ContractCode,
                    ContractDate = addEmployeeDto.ContractDate,
                    Studied = addEmployeeDto.Studied,
                    OperatorHR = addEmployeeDto.OperatorHR,
                    StatutEmployee = addEmployeeDto.StatutEmployee
                };

                await _employeeService.AddEmployee(employeeModel);
                _logger.LogInformation($"New Employee added {addEmployeeDto}");
                return Ok(new { message = "Adaugare angajat nou cu succes", status = "success" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message}, on AddEmployee method");
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
                ContractCode = employee.ContractCode,
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
            try
            {
                var result = await _employeeService.DeleteEmployeeAsync(id);
                if (result)
                {
                    _logger.LogInformation($"Deleted Employee with id[{id}]");
                    return Ok(new { status = "succes" });
                }
                _logger.LogError($"NotFound exmployee with id[{id}] on deleting action");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message}, on DeleteEmployee method");
                return BadRequest();
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeDto employee)
        {
            try
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
                    ContractCode = employee.ContractCode,
                    Studied = employee.Studied,
                    StatutEmployee = employee.StatutEmployee,
                };
                var result = await _employeeService.UpdateEmployeeAsync(id, employeeDto);
                if (result)
                {
                    _logger.LogInformation($"Updated Employee with id[{id}]");
                    return Ok();
                }
                _logger.LogError($"NotFound exmployee with id[{id}] on Updating action");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message}, on UpdateEmployee method");
                return BadRequest();
            }
        }

    }
}
