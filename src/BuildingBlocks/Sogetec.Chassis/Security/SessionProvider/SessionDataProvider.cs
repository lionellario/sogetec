using Microsoft.AspNetCore.Authorization;

namespace Sogetec.Chassis.Security.SessionProvider;

public interface ISessionDataProvider
{
    SessionData GetSessionData();
}

public sealed class SessionDataProvider : ISessionDataProvider
{
    public SessionData SessionData { get; set; } = default!;

    public SessionDataProvider(IHttpContextAccessor httpContextAccessor, ClaimsPrincipal principal)
    {
        var endpoint = httpContextAccessor.HttpContext?.GetEndpoint();

        var requiresAuth = endpoint?.Metadata.GetMetadata<IAuthorizeData>() is not null;
        var allowsAnonymous = endpoint?.Metadata.GetMetadata<IAllowAnonymous>() is not null;

        if (!requiresAuth || allowsAnonymous)
        {
            SessionData = new SessionData(
            SessionId: Guid.NewGuid().ToString(),
            UserId: Guid.NewGuid(),
            TimeZone: "US/NY",
            Locale: "en-US",
            CountryCode: "US");
        }
        else
        {
            var userId = principal.GetAuthenticatedUserId();
            var sessionId = principal.GetAuthenticatedSessionId();

            // TODO: Set the correct value for TZ, locale, country code and AppTier here, either from the claim or the httpContext

            SessionData = new SessionData(
                SessionId: sessionId,
                UserId: userId,
                TimeZone: "US/NY",
                Locale: "en-US",
                CountryCode: "US");
        }
    }

    public SessionData GetSessionData() => SessionData;
}
