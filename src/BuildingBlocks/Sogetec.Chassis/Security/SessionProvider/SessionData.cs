namespace Sogetec.Chassis.Security.SessionProvider;

public record SessionData(
    string SessionId,
    Guid UserId,
    string CountryCode,
    string TimeZone,
    string Locale);
