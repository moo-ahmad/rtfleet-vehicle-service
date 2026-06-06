using System.Net;
using System.Text.Json;

namespace RTFleetVehicleService.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var (statusCode, logLevel) = ex switch
            {
                KeyNotFoundException => (HttpStatusCode.NotFound, LogLevel.Warning),
                UnauthorizedAccessException => (HttpStatusCode.Unauthorized, LogLevel.Warning),
                _ => (HttpStatusCode.InternalServerError, LogLevel.Error)
            };

            _logger.Log(logLevel, ex,
                "{ExceptionType} on {Method} {Path} — {Message}",
                ex.GetType().Name, context.Request.Method, context.Request.Path, ex.Message);

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var response = new
            {
                success = false,
                statusCode = context.Response.StatusCode,
                error = new
                {
                    type = ex.GetType().Name,
                    message = _env.IsDevelopment() ? ex.Message : "An unexpected error occurred. Please try again later.",
                    traceId = context.TraceIdentifier,
                    path = context.Request.Path,
                    method = context.Request.Method,
                    timeStamp = DateTime.UtcNow
                }
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
