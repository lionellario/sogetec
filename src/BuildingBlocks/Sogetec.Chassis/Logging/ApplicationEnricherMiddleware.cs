using Serilog.Context;

namespace Sogetec.Chassis.Logging;

internal sealed class ApplicationEnricherMiddleware() : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (Http.Endpoints.IsExcludedPath(context.Request.Path))
        {
            await next(context);
            return;
        }

        var userId = context.User.GetAuthenticatedUserId();
        using (LogContext.PushProperty("UserId", userId))
        {
            await next(context);
        }
    }
}
