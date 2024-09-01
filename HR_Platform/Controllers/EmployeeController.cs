using HR_PLATFORM.DTOs.CV;
using HR_PLATFORM.DTOs.Employee;
using HR_PLATFORM_APPLICATION.Interface;
using HR_PLATFORM_APPLICATION.Model.Auth;
using HR_PLATFORM_APPLICATION.Model.Employee;
using HR_PLATFORM_APPLICATION.Services;
using Microsoft.AspNetCore.Mvc;

namespace HR_PLATFORM.Controllers
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
                    PhoneNumber = addEmployeeDto.PhoneNumber,
                    Department = addEmployeeDto.Department,
                    Function = addEmployeeDto.Function,
                    ContractCode = addEmployeeDto.ContractCode,
                    ContractDate = addEmployeeDto.ContractDate,
                    Studied = addEmployeeDto.Studied,
                    OperatorHR = addEmployeeDto.OperatorHR,
                    codeManager = addEmployeeDto.codeManager,
                    StatutEmployee = addEmployeeDto.StatutEmployee,
                    Grafic = addEmployeeDto.Grafic,
                };

                await _employeeService.AddEmployee(employeeModel);
                _logger.LogInformation($"New Employee added {addEmployeeDto}");
                return Ok(new { message = "Adaugare angajat nou cu succes", status = "success" });
            }
            catch (ArgumentException ex)
            {
                _logger.LogError($"Exception: {ex.Message}, on AddEmployee method");
                return BadRequest(ex);
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
                PhoneNumber = employee.PhoneNumber,
                Department = employee.Department,
                Function = employee.Function,
                ContractCode = employee.ContractCode,
                ContractDate = employee.ContractDate,
                Studied = employee.Studied,
                OperatorHR = employee.OperatorHR,
                CodeManager = employee.codeManager,
                Grafic = employee.Grafic,
                StatutEmployee = employee.StatutEmployee,
            };

            return Ok(employeeDto);
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

        [HttpGet]
        [Route("GetEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                return Ok(await _employeeService.GetEmployees());

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeDto employee)
        {
            try
            {
                var employeeDto = new EmployeeModelUpdate
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
                    Grafic = employee.Grafic,
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

        [HttpPost("uploadImage")]
        public async Task<IActionResult> UploadUserImage([FromForm] UploadImage dto)
        {
            if (dto.File == null || dto.File.Length == 0)
                return BadRequest("File is empty.");

            using var memoryStream = new MemoryStream();
            await dto.File.CopyToAsync(memoryStream);

            await _employeeService.AddUserImage(dto.File.FileName, memoryStream.ToArray(), dto.EmployeeId, dto.File.ContentType);

            return Ok();
        }

        [HttpGet]
        [Route("GetImage")]
        public async Task<IActionResult> GetImage(int codeEmployee)
        {
            try
            {
                var employeeImage = await _employeeService.GetImage(codeEmployee);
                if (employeeImage == null)
                {
                    return BadRequest();
                }

                var cv = new GetUserImageDTO
                {
                    CodEmployee = employeeImage.CodEmployee,
                    CV_Data = employeeImage.CVData,
                    FileName = employeeImage.FileName,
                    ContentType = employeeImage.ContentType
                };
                //Response.Headers.Add("X-File-Name", employeeImage.FileName);
                return File(cv.CV_Data, cv.ContentType, cv.FileName);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
