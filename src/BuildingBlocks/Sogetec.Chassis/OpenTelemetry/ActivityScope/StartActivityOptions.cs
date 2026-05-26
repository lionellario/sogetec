namespace Sogetec.Chassis.OpenTelemetry.ActivityScope;

public sealed record StartActivityOptions
{
    public const ActivityKind Kind = ActivityKind.Internal;

    public Dictionary<string, object?> Tags { get; set; } = [];

    public string? ParentId { get; set; }

    public ActivityContext? Parent { get; set; }

    public ActivityOptionsStatus? Status { get; set; }
}

public record ActivityOptionsStatus(ActivityStatusCode Code, string? Description);
