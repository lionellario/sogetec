using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Sogetec.Chassis.EF.Interceptors;

public sealed class EventDispatchInterceptor(
    IDomainEventDispatcher dispatcher,
    ISessionDataProvider sp,
    ILogger<EventDispatchInterceptor> logger) : SaveChangesInterceptor
{
    private readonly SessionData _sessionData = sp.GetSessionData();

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var ctx = eventData.Context;

        if (ctx is null)
        {
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        var concurrentEntries = ctx.ChangeTracker
            .Entries()
            .Where(x => x.State is EntityState.Added or EntityState.Modified)
            .Select(e => e.Entity);

        var now = DateTimeOffset.UtcNow;

        foreach (var entry in concurrentEntries)
        {
            // Common audit tracking
            if (entry is Entity entity)
            {
                entity.LastModifiedOn = now;
            }

            // User tracking
            if (entry is DomainEntity tracked)
            {
                tracked.LastModifiedBy = _sessionData.UserId;
            }
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,
        CancellationToken cancellationToken = default
    )
    {
        var ctx = eventData.Context;
        var sessionData = sp.GetSessionData();

        if (ctx is null)
        {
            return await base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        var entitiesWithEvents = ctx
            .ChangeTracker.Entries<IHasDomainEvents>()
            .Select(e => e.Entity)
            .Where(x => x.DomainEvents.Count != 0)
            .ToImmutableList();

        await dispatcher.DispatchAndClearEvents(entitiesWithEvents);

        return await base.SavedChangesAsync(eventData, result, cancellationToken);
    }

    public override async Task SaveChangesFailedAsync(DbContextErrorEventData eventData, CancellationToken cancellationToken = default)
    {
        var ex = eventData.Exception;

        switch (ex)
        {
            case DbUpdateConcurrencyException dcex:
                logger.LogError(dcex, "sessionId={SessionId} DbUpdateConcurrencyException occurred", _sessionData.SessionId);
                break;
            default:
                logger.LogError(ex, "sessionId={SessionId} Commit Operation exception", _sessionData.SessionId);
                break;
        }

        await base.SaveChangesFailedAsync(eventData, cancellationToken);
    }
}
