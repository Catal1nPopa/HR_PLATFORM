﻿using HR_PLATFORM.DTOs.CV;
using HR_PLATFORM_APPLICATION.Interface;
using HR_PLATFORM_APPLICATION.Model.CV;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HR_PLATFORM.Controllers.CV
{
    [Route("api/[controller]")]
    [ApiController]
    public class CVController(ICVService cVService) : ControllerBase
    {
        private readonly ICVService _cvService = cVService;

        //[HttpPost]
        //[Route("AddCV")]
        //public async Task<IActionResult> AddCV([FromBody] CVUploadDto cVUploadDto)
        //{
        //    if (string.IsNullOrEmpty(cVUploadDto.File) || cVUploadDto.FileName == null)
        //        return BadRequest("No file uploaded.");

        //    byte[] fileData = Convert.FromBase64String(cVUploadDto.File);

        //    //var newCV = new CVModel
        //    //{
        //    //    CodeEmployee = cVUploadDto.CodeEmployee,
        //    //    File = cVUploadDto.File
        //    //};

        //    await _cvService.AddCV(cVUploadDto.FileName, fileData, cVUploadDto.CodeEmployee);

        //    return Ok("CV incarcat cu succes");
        //}

        [HttpPost("upload")]
        public async Task<IActionResult> UploadCV([FromForm] UploadCVDto dto)
        {
            if (dto.File == null || dto.File.Length == 0)
                return BadRequest("File is empty.");

            using var memoryStream = new MemoryStream();
            await dto.File.CopyToAsync(memoryStream);

            await _cvService.AddCV(dto.File.FileName, memoryStream.ToArray(), dto.EmployeeId, dto.File.ContentType);

            return Ok();
        }


        [HttpGet]
        [Route("GetCV")]
        public async Task<IActionResult> DownloadCV(int codeEmployee)
        {
            var employeeCV = await _cvService.DownloadCV(codeEmployee);
            if(employeeCV == null)
            {
                return BadRequest();
            }

            var cv = new CVDownloadDTO
            {
                CodEmployee = employeeCV.CodEmployee,
                CV_Data = employeeCV.CVData,
                FileName = employeeCV.FileName,
                ContentType = employeeCV.ContentType
            };
            //Response.Headers.Add("X-File-Name", employeeCV.FileName);
            return File(cv.CV_Data, cv.ContentType, cv.FileName);
        }
    }
}