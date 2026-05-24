namespace BibliotecaApp.Etapa3.Middlewares;

public class RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var startedAt = DateTime.UtcNow;
        await next(context);

        logger.LogInformation("{Method} {Path} => {StatusCode} ({ElapsedMs} ms)",
            context.Request.Method,
            context.Request.Path,
            context.Response.StatusCode,
            (DateTime.UtcNow - startedAt).TotalMilliseconds);
    }
}
