namespace Sogetec.Chassis.Caching;

public static class CacheTag
{
    public const string Tenant = "tenant";
    public const string AccountCategory = "tenant";
    public const string Auth = "auth";
    public const string TenantInfo = "tenant_info";
    public const string UserInfo = "user_info";
    public const string UserPermissions = "user_permissions";

    public static string ForChartOfAccounts(long tenantId) => $"tenant:id={tenantId}:chart-of-accounts";
}
