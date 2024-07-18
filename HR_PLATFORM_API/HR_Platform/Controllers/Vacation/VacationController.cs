using HR_PLATFORM.DTOs.Vacation;
using HR_PLATFORM_APPLICATION.Interface;
using HR_PLATFORM_APPLICATION.Model.Vacation;
using Microsoft.AspNetCore.Mvc;

namespace HR_PLATFORM.Controllers.Vacation
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
                        TypeVacation = vacationDto.TypeVacation
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

                var result = await _vacationService.UpdateVacationAsync( vacationDto);
                if (result) { return Ok(new { message = "Update cu succes", status = "success" }); }

                return BadRequest(new { statut = "error" });
            }
            catch { return BadRequest(new {StatusCode = "error"}); }    
        }

        [HttpGet("GetVacationsEmployees")]
        public async Task<List<VacationModel>> GetVacationsEmployees()
        {
            List<VacationModel> vacationDto = await _vacationService.GetVacationsEmployees();
            if (vacationDto.Count == 0) { return new List<VacationModel>(); };
            return vacationDto;
        }

        [HttpGet("GetVacationsEmployee")]
        public async Task<List<VacationModel>> GetVacationsEmployee(int codeEmployee)
        {
            var vacations = await _vacationService.GetEmployeeVacations(codeEmployee);
            if(vacations.Count == 0) { return new List<VacationModel>(); };
            return vacations;
        }
    }
}
