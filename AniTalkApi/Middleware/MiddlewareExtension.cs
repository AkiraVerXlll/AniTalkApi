using Microsoft.AspNetCore.Connections;

namespace AniTalkApi.Middleware;

public static class MiddlewareExtension
{
    public static void UseExceptionLogger(this WebApplication app)
    {
        app.UseMiddleware<ExceptionLoggerMiddleware>();
    }
}