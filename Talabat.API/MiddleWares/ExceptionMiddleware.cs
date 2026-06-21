using System.Net;
using System.Text.Json;
using Talabat.API.Errors;

namespace Talabat.API.MiddleWares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next?.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);       // Log Error On Console in Development Environment
                                                    // In Production log Exception in Database | File

                // 1. Response Request
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                // 2. Response Body
                var responseBody = _environment.IsDevelopment() ? new ApiExceptionResponse(ex.StackTrace, null, (int)HttpStatusCode.InternalServerError)
                              : new ApiExceptionResponse(null, null, (int)HttpStatusCode.InternalServerError);


                var jsonOptions = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };


                var json = JsonSerializer.Serialize(responseBody, jsonOptions);
                await context.Response.WriteAsync(json);


            }

        }
    }
}
