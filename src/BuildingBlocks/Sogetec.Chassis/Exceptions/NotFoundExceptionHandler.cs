namespace Sogetec.Chassis.Exceptions;

public sealed class NotFoundException(string message, Enum? errorCode = null) : Exception(message)
{
    public Enum ErrorCode => errorCode ?? GenericErrorCode.NotFound;

    public static NotFoundException For<T>(int id, Enum? errorCode = null)
    {
        return For<T>(id.ToString(), errorCode);
    }

    public static NotFoundException For<T>(int? id, Enum? errorCode = null)
    {
        return For<T>(id?.ToString(), errorCode);
    }

    public static NotFoundException For<T>(Guid id, Enum? errorCode = null)
    {
        return For<T>(id.ToString(), errorCode);
    }

    public static NotFoundException For<T>(List<Guid> ids, Enum? errorCode = null)
    {
        return For<T>(ids.ToString(), errorCode);
    }

    public static NotFoundException For<T>(Guid? id, Enum? errorCode = null)
    {
        return For<T>(id?.ToString(), errorCode);
    }

    public static NotFoundException For<T>(string? id, Enum? errorCode = null)
    {
        return new($"{typeof(T).Name} with identifier {id} was not found.", errorCode);
    }
}

internal sealed class NotFoundExceptionHandler(
    ILogger<NotFoundExceptionHandler> logger,
    PerRequestLogBuffer logBuffer
) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        if (exception is not NotFoundException notFoundException)
        {
            return false;
        }

        logger.LogWarning(
            exception,
            "[{Handler}] Not found: {Message}. TraceId: {TraceId}",
            nameof(NotFoundExceptionHandler),
            notFoundException.Message,
            httpContext.TraceIdentifier
        );

        logBuffer.Flush();

        await httpContext
                .WriteProblemAsync(
                    title: notFoundException.ErrorCode.ToDisplayString(),
                    statusCode: StatusCodes.Status404NotFound);

        return true;
    }
}
