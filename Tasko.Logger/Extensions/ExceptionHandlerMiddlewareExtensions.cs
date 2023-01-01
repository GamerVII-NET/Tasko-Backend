using Microsoft.AspNetCore.Builder;
using NLog;
using Tasko.Logger.Middlewares;

namespace Tasko.Logger.Extensions
{
    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static void UseExceptionHandlerMiddleware(this IApplicationBuilder app, ILogger logger)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>(logger);
        }
    }
}
