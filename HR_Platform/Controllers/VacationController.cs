using HR_PLATFORM.DTOs.Vacation;
using HR_PLATFORM_APPLICATION.Interface;
using HR_PLATFORM_APPLICATION.Model.Vacation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR_PLATFORM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacationController : ControllerBase
    {
        private readonly IVacationService _vacationService;
        private readonly IEmployeeService _employeeService;
        public VacationController(IVacationService vacationService, IEmployeeService employeeService)
        {
            _vacationService = vacationService;
            _employeeService = employeeService;
        }

        //[Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> AddVacation([FromBody] VacationDto vacationDto)
        {
            try
            {
                var checkEmployee = await _employeeService.GetEmployeeByIdAsync(vacationDto.CodEmployee);
                if (checkEmployee != null)
                {
                    var vacationModel = new VacationModel
                    {
                        CodEmployee = vacationDto.CodEmployee,
                        StartDate = vacationDto.StartDate,
                        EndDate = vacationDto.StartDate.AddDays(vacationDto.DaysVacation),
                        DaysVacation = vacationDto.DaysVacation,
                        VacationDaysLeft = vacationDto.VacationDaysLeft,
                        CodeManager = vacationDto.CodeManager,
                        TypeVacation = vacationDto.TypeVacation,
                        Status = vacationDto.Status,
                    };

                    await _vacationService.AddVacationAsync(vacationModel);
                    return Ok(new { message = "Adaugare concediu cu succes", status = "success" });
                }
                return BadRequest(new { status = "error" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message, status = "error" });
            }
        }

        [Authorize]
        [HttpPatch("updateVcation")]
        public async Task<IActionResult> UpdateVacation([FromBody] VacationDto vacation)
        {
            try
            {
                var vacationDto = new VacationModel
                {
                    Id = vacation.Id,
                    CodEmployee = vacation.CodEmployee,
                    StartDate = vacation.StartDate,
                    EndDate = vacation.EndDate,
                    DaysVacation = vacation.DaysVacation,
                    TypeVacation = vacation.TypeVacation,
                    VacationDaysLeft = vacation.VacationDaysLeft
                };

                var result = await _vacationService.UpdateVacationAsync(vacationDto);
                if (result) { return Ok(new { message = "Update cu succes", status = "success" }); }

                return BadRequest(new { statut = "error" });
            }
            catch { return BadRequest(new { StatusCode = "error" }); }
        }

        //[Authorize(Policy = "admin")]
        [HttpGet("GetVacationsEmployees")]
        public async Task<IActionResult> GetVacationsEmployees()
        {
            try
            {
                return Ok(await _vacationService.GetVacationsEmployees());
            }catch (Exception ex)   
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetVacationsEmployee")]
        public async Task<IActionResult> GetVacationsEmployee(int codeEmployee)
        {
            try
            {
                return Ok(await _vacationService.GetEmployeeVacations(codeEmployee));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
