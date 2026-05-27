namespace Sogetec.Constants.Aspire;

public static class Services
{
    public const string WebApi = "sogetec-api";
    public const string SPA = "sogetec-frontend";
    public const string ADMIN = "sogetec-admin";
    public const string Gateway = "gateway";


    public static string ToClientName(string application, string? suffix = null)
    {
        var clientName = char.ToUpperInvariant(application[0]) + application[1..];
        return string.IsNullOrWhiteSpace(suffix) ? clientName : $"{clientName} {suffix}";
    }
}
