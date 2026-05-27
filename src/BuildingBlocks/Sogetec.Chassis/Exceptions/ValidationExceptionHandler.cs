namespace Sogetec.Chassis.Exceptions;

internal sealed class ValidationExceptionHandler(
    ILogger<ValidationExceptionHandler> logger,
    PerRequestLogBuffer logBuffer
) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        if (exception is not ValidationException validationException)
        {
            return false;
        }

        var failedFields = validationException.Errors.Select(e => $"{e.PropertyName}:{e.ErrorCode}");

        logger.LogWarning(
            "[{Handler}] Validation failed for fields: {Fields}. TraceId: {TraceId}",
            nameof(ValidationExceptionHandler),
            string.Join(", ", failedFields),
            httpContext.TraceIdentifier
        );

        logBuffer.Flush();

        var errors = validationException
            .Errors.GroupBy(e => e.ErrorCode)
            .ToDictionary(g => g.Key, g => g.Select(e => e.PropertyName ?? e.ErrorMessage).ToArray());

        await TypedResults
                .ValidationProblem(
                    title: "One or more validation errors occurred.",
                    errors: errors,
                    type: ProblemExtensions.GetProblemType(StatusCodes.Status400BadRequest))
                .ExecuteAsync(httpContext);

        return true;
    }
}
