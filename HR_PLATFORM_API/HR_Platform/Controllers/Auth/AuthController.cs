using HR_PLATFORM.DTOs.Auth;
using HR_PLATFORM_APPLICATION.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_PLATFORM.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        //[Authorize(Roles = "admin,user")]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var token = await _authService.AuthenticateAsync(loginDto.Username, loginDto.Password);
                if (token == null)
                {
                    return Unauthorized();
                }
                return Ok(new { token, message = "Logare cu succes", status = "success" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message, status = "error"});
            }
        }

        [Authorize]
        [HttpPost("addEmployeeLogin")]
        public async Task<IActionResult> AddEmployeeLogin([FromBody] AddNewLogin registerDto)
        {
            try
            {
                await _authService.CreateUserAsync(registerDto.Username, registerDto.Password, registerDto.Role);
                return Ok(new { message = "Utilizator creat cu succes", newStatus = "success" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message, status = "error" });
            }
        }
    }
}
