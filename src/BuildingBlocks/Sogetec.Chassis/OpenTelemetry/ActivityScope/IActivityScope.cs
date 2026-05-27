namespace Sogetec.Chassis.OpenTelemetry.ActivityScope;

public interface IActivityScope
{
    Activity? Start(string name, StartActivityOptions options);

    Task Run(
        string name,
        Func<Activity?, CancellationToken, Task> run,
        StartActivityOptions options,
        CancellationToken ct
    );

    Task<TResult> Run<TResult>(
        string name,
        Func<Activity?, CancellationToken, Task<TResult>> run,
        StartActivityOptions options,
        CancellationToken ct
    );
}
