﻿using HR_PLATFORM.DTOs.Auth;
using HR_PLATFORM_APPLICATION.Interface;
using HR_PLATFORM_APPLICATION.Model.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog.Context;

namespace HR_PLATFORM.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService, ILogger<AuthController> logger) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly ILogger<AuthController> _logger = logger;

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
                    _logger.LogError($"User Unauthorized { loginDto.Username }");
                    return Unauthorized();
                }
                if (authResult.IsFirstLogin)
                {
                    return Ok(new { status = "ChangePass" });
                }
                _logger.LogInformation($"User Authorized {loginDto.Username} on Login Controller");
                //LogContext.PushProperty("User Authorized", loginDto.Username);
                return Ok(authResult.Token);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message}, on Login method");
                return BadRequest(new { ex.Message, status = "error"});
            }
        }

        //[Authorize(Policy = "admin")]
        [HttpGet]
        [Route("test")]
        public async Task<String> getTest()
        {
            return "test";
        }

        [HttpGet]
        [Route("Users")]
        public async Task<List<UsersModel>> getUsers()
        {
            return await _authService.GetUsers();
        }

        //[Authorize(Policy = "admin")]
        [HttpPost]
        [Route("addLogin")]
        public async Task<IActionResult> AddEmployeeLogin([FromBody] AddNewLogin registerDto)
        {
            try
            {
                var response = await _authService.CreateUserAsync(registerDto.Username, registerDto.Password, registerDto.Role, registerDto.codeEmployee);
                if (response)
                {
                    _logger.LogInformation($"New user created: Username: {registerDto.Username}, Role: {registerDto.Role}");
                    return Ok(new { message = "Utilizator creat cu succes", newStatus = "success" });
                }
                else
                {
                    return BadRequest(new { status = "error", Message = "Utilizatorul nu a fost creat" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message}, on AddEmployee method");
                return BadRequest(new { ex.Message, status = "error" });
            }
        }

        //[Authorize]
        [HttpPatch]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword(string username, string newPassword, string codeEmployee)
        {
            try
            {
                var result = await _authService.ChangeUserPassword(username, newPassword, codeEmployee);
                if(result)
                {
                    _logger.LogInformation($"Changed password: {username}");
                    return Ok(new { message = "Schimbă parola pentru utilizatorul: " + username });
                }
                else
                {
                    return BadRequest(new { Message = "Eroare la schimbare parola", status = "error" });
                }
            }
            catch(Exception ex) 
            {
                _logger.LogError($"Exception: {ex.Message}, on ChangePassword");
                return BadRequest(new { ex.Message, status = "error" });
            }
        }


        [HttpDelete]
        [Route("DeleteLogin")]
        public async Task<IActionResult> DeleteLogin(string username)
        {
            try
            {
                await _authService.DeleteUserLogin(username);
                return Ok();
            } 
            catch(Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
