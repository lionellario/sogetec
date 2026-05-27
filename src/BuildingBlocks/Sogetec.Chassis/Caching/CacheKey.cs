namespace Sogetec.Chassis.Caching;

public static class CacheKey
{
    public static string ForPermissions(long tenantId, string rolesHash) => $"permissions:tenant-id={tenantId}:roles-hash={rolesHash}";

    public static string ForUserInfo(long tenantId, Guid userId) => $"user_information:tenant-id={tenantId}:user_id={userId}";

    public static string ForTenantInfoByDomain(string domain) => $"tenant-information:domain={domain.ToLower()}";

    public static string ForChartOfAccounts(long tenantId) => $"accounting:chart-of-accounts:tenant-id={tenantId}";

    public static string ForAccessToken() => "keycloak:admin-access-token";
}
