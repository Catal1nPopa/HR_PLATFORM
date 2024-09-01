using HR_PLATFORM.DTOs.Employee;
using HR_PLATFORM_APPLICATION.Interface;
using HR_PLATFORM_APPLICATION.Model.Salary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_PLATFORM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryController(ILogger<SalaryController> logger, ISalaryService salaryService) : ControllerBase
    {
        private readonly ILogger<SalaryController> _logger = logger;
        private readonly ISalaryService _salaryService = salaryService;
        [HttpPost("AddSalary")]
        public async Task<IActionResult> AddSalary([FromBody] SalaryDto salaryDto)
        {
            try
            {
                var salaryModel = new SalaryModel
                {
                    CodeEmployee = salaryDto.CodeEmployee,
                    SalaryEmployee = salaryDto.Salary,
                    SalaryPerDay = salaryDto.SalaryPerDay,
                    DateTime = DateTime.Now,
                };
                await _salaryService.AddEmployeeSalary(salaryModel);
                return Ok(salaryModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteSalary")]
        public async Task<IActionResult> DeleteEmployeeSalary(int codeEmployee, DateTime dateTime)
        {
            try
            {
                await _salaryService.DeleteEmployeeSalary(codeEmployee, dateTime);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Could not delete {ex.Message}");
            }
        }

        [HttpGet("GetEmployeeSalary")]
        public async Task<IActionResult> GetEmployeeSalary(int codeEmployee)
        {
            try
            {
                var result = await _salaryService.GetEmployeeSalary(codeEmployee);
                return Ok(new SalaryDto
                {
                    CodeEmployee = result.CodeEmployee,
                    Salary = result.SalaryEmployee,
                    SalaryPerDay = result.SalaryPerDay,
                    DateTime = result.DateTime,
                });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetEmployeesSalary")]
        public async Task<IActionResult> GetEmployeesSalary()
        {
            try
            {
                return Ok(await _salaryService.GetEmployeesSalary());
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetSalaryHistory")]
        public async Task<IActionResult> GetSalary(int codeEmployee, DateTime startDate, DateTime endDate)
        {
            try
            {
                return Ok(await _salaryService.GetSalaryHistory(codeEmployee, startDate, endDate));
            }
             catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetSalaryHistoryAll")]
        public async Task<IActionResult> GetSalaryHistoryAll(DateTime startDate, DateTime endDate)
        {
            try
            {
                return Ok(await _salaryService.GetSalaryHistoryAll(startDate, endDate));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
