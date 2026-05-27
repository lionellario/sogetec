namespace Sogetec.Chassis.Exceptions;

public sealed class ConflictException(string message, Enum? errorCode = null) : InvalidOperationException(message)
{
    public Enum ErrorCode => errorCode ?? GenericErrorCode.InvalidOperation;

    public static ConflictException For<T>(Guid? id, Enum? errorCode = null)
    {
        return For<T>(id?.ToString(), errorCode);
    }

    public static ConflictException For<T>(string? id, Enum? errorCode = null)
    {
        return string.IsNullOrWhiteSpace(id)
            ? new($"Operation failed: {typeof(T).Name}.", errorCode)
            : new($"Operation failed: {typeof(T).Name} with identifier {id}.", errorCode);
    }

    public static ConflictException For<T>(Enum errorCode)
    {
        return new($"Operation failed: {typeof(T).Name}.", errorCode);
    }
}

internal sealed class InvalidOperationExceptionHandler(
    ILogger<InvalidOperationExceptionHandler> logger,
    PerRequestLogBuffer logBuffer
) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        if (exception is not ConflictException notFoundException)
        {
            return false;
        }

        logger.LogWarning(
            exception,
            "[{Handler}] Dependency conflict: {Message}. TraceId: {TraceId}",
            nameof(InvalidOperationExceptionHandler),
            notFoundException.Message,
            httpContext.TraceIdentifier
        );

        logBuffer.Flush();

        await httpContext
                .WriteProblemAsync(
                    title: notFoundException.ErrorCode.ToDisplayString(),
                    statusCode: StatusCodes.Status409Conflict
                );

        return true;
    }
}
