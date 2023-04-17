using DataAcessLayer.Entity;
using Serilog;

namespace CleanArchitecture.GlobalException
{
    public class GlobalExceptionHandling
    {
        private readonly RequestDelegate _next;
        //private readonly ILogger<GlobalExceptionHandlling> _logger;

        public GlobalExceptionHandling(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                //Log.Information("");
                await _next(context);
            }
            catch (Exception exe)
            {
                Log.Error(exe, exe.Message);

                await HandleExceptionAsync(context, exe);
            }
            Log.CloseAndFlush();
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            int statusCode;

            statusCode = exception switch
            {
                AccessViolationException => StatusCodes.Status404NotFound,
                DivideByZeroException => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError,
            };

            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = statusCode,
                Message = exception.Message
            }.ToString());
        }
    }
}