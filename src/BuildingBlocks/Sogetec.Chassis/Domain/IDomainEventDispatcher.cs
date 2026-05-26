using System.Collections.Immutable;

namespace Sogetec.Chassis.Domain;

public interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents(ImmutableList<IHasDomainEvents> entitiesWithEvents);
}
