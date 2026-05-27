namespace Sogetec.Chassis.CQRS.Pipelines;

public sealed class LoggingBehavior<TMessage, TResponse>(ILogger<LoggingBehavior<TMessage, TResponse>> logger)
    : IPipelineBehavior<TMessage, TResponse> where TMessage : IMessage
{
    public async ValueTask<TResponse> Handle(
        TMessage message,
        MessageHandlerDelegate<TMessage, TResponse> next,
        CancellationToken cancellationToken
    )
    {
        const string behavior = nameof(LoggingBehavior<,>);

        logger.LogInformation(
                "[{Behavior}] Handle request={Request} and response={Response}",
                behavior,
                message.GetType().GetFriendlyName(),
                typeof(TResponse).GetFriendlyName()
            );

        var props = new List<PropertyInfo>(message.GetType().GetProperties());
        foreach (var prop in props)
        {
            var propValue = prop.GetValue(message, null);
            logger.LogInformation(
                "[{Behavior}] Property {Property} : {@Value}",
                behavior,
                prop.Name,
                propValue
            );
        }

        var start = Stopwatch.GetTimestamp();

        var response = await next(message, cancellationToken);

        var timeTaken = Stopwatch.GetElapsedTime(start);

        const int THRESHOLD = 3;

        if (timeTaken.Seconds >= THRESHOLD)
        {
            logger.LogWarning(
                "[{Behavior}] The request {Request} took {TimeTaken} seconds.",
                behavior,
                message.GetType().GetFriendlyName(),
                timeTaken.Seconds
            );
        }
        else
        {
            logger.LogInformation(
                "[{Behavior}] The request handled {RequestName} in {ElapsedMilliseconds} ms",
                behavior,
                message.GetType().GetFriendlyName(),
                Stopwatch.GetElapsedTime(start).TotalMilliseconds
            );
        }

        return response;
    }
}
