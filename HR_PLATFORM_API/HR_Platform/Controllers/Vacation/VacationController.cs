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
        public VacationController(IVacationService vacationService)
        {
            _vacationService = vacationService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddVacation([FromBody] VacationDto vacationDto)
        {
            try
            {
                var vacationModel = new VacationModel
                {
                    CodEmployee = vacationDto.CodEmployee,
                    StartDate = vacationDto.StartDate,
                    EndDate = vacationDto.EndDate,
                    DaysVacation = vacationDto.DaysVacation,
                    VacationDaysLeft = vacationDto.VacationDaysLeft,
                    TypeVacation = vacationDto.TypeVacation
                };

                await _vacationService.AddVacationAsync(vacationModel);
                return Ok(new { message = "Adaugare concediu cu succes", status = "success" });
            } catch (Exception ex)
            {
                return BadRequest(new { ex.Message, status = "error" });
            }
        }
    }
}
