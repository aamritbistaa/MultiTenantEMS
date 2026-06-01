using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MultiTenantEMS.API.Middleware
{
    /// <summary>
    /// Handles an unhandled exception and writes a standardized error response.
    /// </summary>
    /// <param name="httpContext">The HTTP context for the current request.</param>
    /// <param name="exception">The exception to handle.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>True if the exception was handled.</returns>
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IProblemDetailsService problemDetailsService)
        : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var env = httpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();

            var isDev = env.IsDevelopment();

            logger.LogError(exception, "Unhandled exception occurred");

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = new ProblemDetails
                {
                    Type = "https://httpstatuses.com/500",
                    Title = "Internal Server Error",
                    Detail = isDev ? exception.Message : "An unexpected error occurred.",
                    Status = StatusCodes.Status500InternalServerError,
                    Instance = httpContext.Request.Path
                },
            });
            return true;
        }
    }
}
