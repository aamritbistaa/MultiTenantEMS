using Microsoft.AspNetCore.Mvc;
using MultiTenantEMS.Application.Common;

namespace MultiTenantEMS.API.Extensions
{
    public static class ApiBehaviorExtensions
    {
        public static IServiceCollection AddCustomApiBehaviors(this IServiceCollection services)
        {
            services.AddProblemDetails(c =>
            {
                c.CustomizeProblemDetails = context =>
                {
                    context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);
                };
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    var errorMessage = string.Join("; ", errors);
                    var result = Result.Failure(errorMessage);
                    
                    return new BadRequestObjectResult(result);
                };
            });

            return services;
        }
    }
}
