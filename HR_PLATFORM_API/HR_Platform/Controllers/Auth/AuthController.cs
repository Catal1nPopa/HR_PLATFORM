﻿using HR_PLATFORM.DTOs.Auth;
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
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var authResult = await _authService.AuthenticateAsync(loginDto.Username, loginDto.Password);
                if (authResult == null)
                {
                    return Unauthorized();
                }
                if (authResult.IsFirstLogin)
                {
                    return RedirectToAction("ChangePassword", "Auth", new { username = loginDto.Username, newPassword = "temporaryPassword" });
                }
                return Ok(new { token = authResult.Token, message = "Logare cu succes", status = "success" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message, status = "error"});
            }
        }

        [Authorize(Policy = "admin")]
        [HttpPost]
        [Route("addLogin")]
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

        [HttpGet]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(string username, string newPassword)
        {
            try
            {
                await _authService.ChangeUserPassword(username, newPassword);
                return Ok(new { message = "Schimbă parola pentru utilizatorul: " + username });
            }
            catch(Exception ex) 
            {
                return BadRequest(new { ex.Message, status = "error" });
            }
        }
    }
}
