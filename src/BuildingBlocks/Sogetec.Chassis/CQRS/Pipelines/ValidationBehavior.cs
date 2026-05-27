using FluentValidation.Results;

namespace Sogetec.Chassis.CQRS.Pipelines;

internal class ValidationBehavior<TMessage, TResponse>(
    IActivityScope activityScope,
    IEnumerable<IValidator<TMessage>> validators,
    ILogger<ValidationBehavior<TMessage, TResponse>> logger
) : MessagePreProcessor<TMessage, TResponse>
    where TMessage : IMessage
    where TResponse : notnull
{
    protected override async ValueTask Handle(TMessage message, CancellationToken cancellationToken)
    {
        const string behavior = nameof(ValidationBehavior<,>);

        logger.LogInformation(
            "[{Behavior}] handle request={RequestData} and response={ResponseData}",
            behavior,
            message.GetType().GetFriendlyName(),
            typeof(TResponse).GetFriendlyName()
        );

        if (!validators.Any())
        {
            return;
        }

        var context = new ValidationContext<TMessage>(message);

        var validationResult = await Task.WhenAll(
            validators.Select(v => v.ValidateAsync(context, cancellationToken))
        );

        var errors = validationResult
            .Where(result => !result.IsValid)
            .SelectMany(result => result.Errors)
            .Select(failure => new ValidationFailure
            {
                ErrorCode = failure.ErrorCode,
                ErrorMessage = failure.ErrorMessage,
                PropertyName = failure.PropertyName
            })
            .ToList();

        if (errors.Count != 0)
        {
            throw new ValidationException(errors);
        }

        var messageType = message.GetType().Name;
        var validatorNames = validators.Aggregate(
            string.Empty,
            (c, x) => $"{x.GetType().Name}, {c}"
        );
        var activityName = $"{messageType}-{validatorNames.Trim().TrimEnd(',')}";

        await activityScope.Run(
            activityName,
            (_, _) => Task.CompletedTask,
            new() { Tags = { { TelemetryTags.Validator.Validation, messageType } } },
            cancellationToken
        );
    }
}
