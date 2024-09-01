using HR_PLATFORM.DTOs.Time;
using HR_PLATFORM_APPLICATION.Interface;
using HR_PLATFORM_APPLICATION.Model.Timex;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_PLATFORM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimexController(IReportingTimex reportingTimex) : ControllerBase
    {
        private readonly IReportingTimex _reportingTime = reportingTimex;

        [HttpPost]
        public async Task<IActionResult> AddTimex(TimexDto timexData)
            {
            TimeSpan timeOnWork = timexData.TimeLastExit - timexData.TimeFirstEntry;
            try
            {
                var timedDto = new ReportTimexModel(
                    timexData.CodeEmployee,
                    timexData.TimeFirstEntry,
                    timexData.TimeLastExit,
                    timexData.LocationEntry,
                    timexData.LocationExit,
                    $"{timeOnWork.Days}zile {timeOnWork.Hours}h {timeOnWork.Minutes}min {timeOnWork.Seconds}sec");
                    await _reportingTime.AddReportingTimex(timedDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetAllTimex")]
        public async Task<IActionResult> GetAllTimex()
        {
            try
            {
               return Ok(await _reportingTime.GetAllReportingTimex());
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllTimexEmployee{codeEmployee}")]
        public async Task<IActionResult> GetAllTimexEmployee(int codeEmployee)
        {
            try
            {
                return Ok(await _reportingTime.GetReportingTimexByEmployee(codeEmployee));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateTime(TimexDto timexDto)
        {
            try
            {
                var model = new ReportTimexModel(
                    timexDto.CodeEmployee,
                    timexDto.TimeFirstEntry,
                    timexDto.TimeLastExit,
                    timexDto.LocationEntry,
                    timexDto.LocationExit,
                    "0");
                await _reportingTime.UpdateReportingTimex(model);
                return Ok();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
