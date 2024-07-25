using System.Text.Json;
using Talabat_.APIS.Errors;

namespace Talabat_.APIS.MiddleWares
{
    public class ExceptionMiddleWares
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleWares> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleWares(RequestDelegate next, ILogger<ExceptionMiddleWares> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;

                var response = _env.IsDevelopment() ?
                    new ApiExceptionResponse(500, ex.Message, ex.StackTrace.ToString())
                    : new ApiExceptionResponse(500);

                var json = JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
