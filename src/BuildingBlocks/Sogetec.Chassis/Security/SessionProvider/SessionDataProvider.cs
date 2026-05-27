namespace Sogetec.Chassis.Security.SessionProvider;

public interface ISessionDataProvider
{
    SessionData GetSessionData();
}

public sealed class SessionDataProvider : ISessionDataProvider
{
    public SessionData SessionData { get; set; } = default!;

    public SessionDataProvider(ClaimsPrincipal principal)
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

    public SessionData GetSessionData() => SessionData;
}
