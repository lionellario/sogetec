using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Sogetec.Chassis.Domain;

public interface IHasDomainEvents
{
    IReadOnlyCollection<DomainEvent> DomainEvents { get; }
}

public abstract class HasDomainEvents : IHasDomainEvents
{
    private readonly List<DomainEvent> _domainEvents = [];

    [NotMapped]
    [JsonIgnore]
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void RegisterDomainEvent(DomainEvent domainEvent)
    {
        if (!_domainEvents.Contains(domainEvent))
            _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(DomainEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
