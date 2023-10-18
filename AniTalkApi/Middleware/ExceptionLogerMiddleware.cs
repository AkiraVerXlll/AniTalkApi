namespace AniTalkApi.Middleware;

public class ExceptionLoggerMiddleware
{
    private readonly RequestDelegate _next;

    private readonly ILogger _logger;

    public ExceptionLoggerMiddleware(
        RequestDelegate next, 
        ILogger logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            switch (exception)
            {
                case ArgumentNullException:
                    _logger.LogError("Error");
                    break;
                default:
                    _logger.LogCritical("");
                    break;
            }
        }
    }
}