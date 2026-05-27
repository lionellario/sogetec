using Sogetec.Chassis.CQRS.Command;
using Sogetec.Chassis.CQRS.Query;

namespace Sogetec.Chassis.CQRS.Pipelines;

public sealed class ActivityBehavior<TMessage, TResponse>(
    IActivityScope activityScope,
    CommandHandlerMetrics commandMetrics,
    QueryHandlerMetrics queryMetrics,
    ILogger<ActivityBehavior<TMessage, TResponse>> logger
) : IPipelineBehavior<TMessage, TResponse>
    where TMessage : IMessage
    where TResponse : notnull
{
    public async ValueTask<TResponse> Handle(
        TMessage message,
        MessageHandlerDelegate<TMessage, TResponse> next,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation(
            "[{Behavior}] handle request={RequestData} and response={ResponseData}",
            nameof(ActivityBehavior<,>),
            message.GetType().GetFriendlyName(),
            typeof(TResponse).GetFriendlyName()
        );

        var attr = message.GetType().GetCustomAttribute<IgnoreOTelOnHandlerAttribute>();

        if (attr is not null)
        {
            return await next(message, cancellationToken);
        }

        var messageType = message.GetType().GetFriendlyName();
        var handlerName = $"{messageType}Handler";
        var activityName = $"{handlerName}/{messageType}";

        var isCommand = messageType.ToLowerInvariant().EndsWith(nameof(Command).ToLowerInvariant());
        var tagName = isCommand ? TelemetryTags.Commands.Command : TelemetryTags.Queries.Query;

        var startingTimestamp = isCommand
            ? commandMetrics.CommandHandlingStart(handlerName)
            : queryMetrics.QueryHandlingStart(handlerName);

        try
        {
            return await activityScope.Run(
                activityName,
                async (_, ct) => await next(message, ct),
                new() { Tags = { { tagName, messageType } } },
                cancellationToken
            );
        }
        finally
        {
            if (isCommand)
            {
                commandMetrics.CommandHandlingEnd(handlerName, startingTimestamp);
            }
            else
            {
                queryMetrics.QueryHandlingEnd(handlerName, startingTimestamp);
            }
        }
    }
}
