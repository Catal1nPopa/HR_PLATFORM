using HR_PLATFORM_DOMAIN.Exceptions;
using Microsoft.AspNetCore.Http;
using Serilog;
using System.Net;
using System.Text.Json;
using KeyNotFoundException = HR_PLATFORM_DOMAIN.Exceptions.KeyNotFoundException;
using NotImplementedException = HR_PLATFORM_DOMAIN.Exceptions.NotImplementedException;
using UnauthorizedAccessException = HR_PLATFORM_DOMAIN.Exceptions.UnauthorizedAccessException;

namespace HR_PLATFORM_APPLICATION.ExceptionConfig
{
    public class ErrorHandlingMiddleware(RequestDelegate requestDelegate)
    {
        private readonly RequestDelegate _requestDelegate = requestDelegate;
        
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        public static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode statusCode;

            var exceptionType = ex.GetType();
            string message = ex.Message;

            switch(exceptionType.Name)
            {
                case nameof(BadRequestException): statusCode = HttpStatusCode.BadRequest; break;
                case nameof(NotFoundException): statusCode = HttpStatusCode.NotFound; break;
                case nameof(NotImplementedException): statusCode = HttpStatusCode.NotImplemented; break;
                case nameof(UnauthorizedAccessException): statusCode = HttpStatusCode.Unauthorized; break;
                case nameof(NoContentException): statusCode = HttpStatusCode.NoContent; break;
                case nameof(KeyNotFoundException): statusCode = HttpStatusCode.Unauthorized; break;
                default: statusCode = HttpStatusCode.InternalServerError; break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            Log.Information($"Error handler test:  {message}");
            return context.Response.WriteAsync(JsonSerializer.Serialize(new { message, statusCode }));
        }
    }
}
