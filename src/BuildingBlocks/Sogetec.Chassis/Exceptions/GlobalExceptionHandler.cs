namespace Sogetec.Chassis.Exceptions;

public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> Logger, GlobalLogBuffer logBuffer, IHostEnvironment env) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

        Logger.LogError(
            exception,
            "[{Handler}] Could not process a request on machine {MachineName}. TraceId: {TraceId}",
            nameof(GlobalExceptionHandler),
            Environment.MachineName,
            traceId
        );

        logBuffer.Flush();

        var (statusCode, title) = MapException(exception);
        var detail = GetSafeErrorMessage(exception, env);

        await httpContext.WriteProblemAsync(
            title: title,
            statusCode: statusCode,
            detail: detail);

        return true;
    }

    private static (int StatusCode, string Title) MapException(Exception exception) => exception switch
    {
        // DomainException appEx => ((int)appEx.StatusCode, appEx.Message),
        ArgumentOutOfRangeException or ArgumentNullException or ArgumentException => (
                StatusCodes.Status400BadRequest,
                "The request contains invalid or missing parameters."
            ),
        InvalidOperationException or NotSupportedException => (
            StatusCodes.Status409Conflict,
            "The request conflicts with the current state of the resource."
        ),
        OperationCanceledException => (StatusCodes.Status408RequestTimeout, "Request time out."),
        UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "Unauthorized"),
        _ => (StatusCodes.Status500InternalServerError, "We made a mistake but we are on it!")
    };

    private static string? GetSafeErrorMessage(Exception exception, IHostEnvironment environment)
    {
        // Only expose details in development
        if (environment.IsDevelopment())
        {
            return exception.Message;
        }

        // In production, only expose messages from our own exceptions
        // return exception is DomainException ? exception.Message : null;
        return null;
    }
}
