using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;

namespace OnlineMedicineBookingApplication.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Call the next middleware
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

           var statusCode = exception switch
            {
                ArgumentNullException or ArgumentException => HttpStatusCode.BadRequest,
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                SqlException => HttpStatusCode.ServiceUnavailable,
                TimeoutException => HttpStatusCode.RequestTimeout,
                KeyNotFoundException => HttpStatusCode.NotFound,
                _ => HttpStatusCode.InternalServerError
            };

            var errorResponse = new 
            {
                StatusCode = (int)statusCode,
                Message = exception.Message,
                Error = exception.GetType().Name
            };

            context.Response.StatusCode = (int)statusCode;
            var json = JsonSerializer.Serialize(errorResponse);

            return context.Response.WriteAsync(json);
        }
    }
}
