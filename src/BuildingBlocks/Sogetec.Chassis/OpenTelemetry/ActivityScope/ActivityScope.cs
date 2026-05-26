namespace Sogetec.Chassis.OpenTelemetry.ActivityScope;

internal sealed class ActivityScope : IActivityScope
{
    public Activity? Start(string name, StartActivityOptions options)
    {
        var activity = options.Parent.HasValue
            ? ActivitySourceProvider
                .Instance.CreateActivity(
                    $"{ActivitySourceProvider.DefaultSourceName}.{name}",
                    StartActivityOptions.Kind,
                    options.Parent.Value,
                    idFormat: ActivityIdFormat.W3C,
                    tags: options.Tags
                )?.Start()
            : ActivitySourceProvider
                .Instance.CreateActivity(
                    $"{ActivitySourceProvider.DefaultSourceName}.{name}",
                    StartActivityOptions.Kind,
                    options.ParentId ?? Activity.Current?.ParentId,
                    idFormat: ActivityIdFormat.W3C,
                    tags: options.Tags
                )?.Start();

        return activity;
    }

    public async Task Run(
        string name,
        Func<Activity?, CancellationToken, Task> run,
        StartActivityOptions options,
        CancellationToken ct
    )
    {
        using var activity = Start(name, options) ?? Activity.Current;

        try
        {
            await run(activity, ct);
            var code = options.Status?.Code ?? ActivityStatusCode.Ok;
            activity?.SetStatus(code, options.Status?.Description);
        }
        catch (Exception ex)
        {
            activity?.SetStatus(ActivityStatusCode.Error);
            activity?.AddException(ex);
            throw;
        }
    }

    public async Task<TResult> Run<TResult>(
        string name,
        Func<Activity?, CancellationToken, Task<TResult>> run,
        StartActivityOptions options,
        CancellationToken ct
    )
    {
        using var activity = Start(name, options) ?? Activity.Current;

        try
        {
            var result = await run(activity, ct);

            var code = options.Status?.Code ?? ActivityStatusCode.Ok;
            activity?.SetStatus(code, options.Status?.Description);

            return result;
        }
        catch (Exception ex)
        {
            if (activity is null)
            {
                throw;
            }

            activity.SetStatus(ActivityStatusCode.Error);
            activity.SetTag("otel.status_code", "error");
            activity.SetTag("otel.status_description", ex.Message);
            activity.AddException(ex);
            throw;
        }
    }
}
