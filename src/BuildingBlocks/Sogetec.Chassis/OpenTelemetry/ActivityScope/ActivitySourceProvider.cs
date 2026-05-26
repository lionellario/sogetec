namespace Sogetec.Chassis.OpenTelemetry.ActivityScope;

public static class ActivitySourceProvider
{
    public const string DefaultSourceName = nameof(Sogetec);

    public static readonly ActivitySource Instance = new(DefaultSourceName, "v1");
}
