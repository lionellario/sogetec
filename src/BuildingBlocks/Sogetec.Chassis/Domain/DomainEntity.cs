namespace Sogetec.Chassis.Domain;

public abstract class Entity : HasDomainEvents
{
    public Guid Id { get; protected set; }

    public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;

    public DateTimeOffset LastModifiedOn { get; set; }

    protected Entity()
    {
        Id = Guid.CreateVersion7();
    }
}

public abstract class DomainEntity : Entity
{
    public Guid LastModifiedBy { get; set; }

    protected DomainEntity()
    {
        Id = Guid.CreateVersion7();
    }
}