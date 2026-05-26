namespace Sogetec.Chassis.Domain;

public abstract class DomainEvent : INotification
{
    public Guid Id { get; protected set; } = Guid.CreateVersion7();

    public DateTimeOffset DateOccurred { get; protected set; } = DateTimeOffset.UtcNow;

    public override bool Equals(object? obj) => obj is DomainEvent at && Id == at.Id;

    public override int GetHashCode() => Id.GetHashCode();
}
