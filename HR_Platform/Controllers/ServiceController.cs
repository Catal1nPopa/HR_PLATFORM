using HR_PLATFORM_APPLICATION.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_PLATFORM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController(IServiceFunction service, ILogger<ServiceController> logger) : ControllerBase
    {
        private readonly IServiceFunction _service = service;
        private readonly ILogger<ServiceController> _logger = logger;

        [HttpGet("GetEmployeeManager")]
        public async Task<IActionResult> Get(int codeEmployee)
        {
            try
            {
                return Ok( await _service.GetEmployeeManager(codeEmployee));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
