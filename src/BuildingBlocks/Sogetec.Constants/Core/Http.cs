using System.Collections.Frozen;
using Microsoft.AspNetCore.Http;

namespace Sogetec.Constants.Core;

public static class Http
{
    public const string RequestIdHeader = "x-request-id";

    public static class Endpoints
    {
        public const string DevUIEndpointPath = "/devui";
        public const string HealthEndpointPath = "/health";
        public const string HealthZEndpointPath = "/healthz";
        public const string AlivenessEndpointPath = "/alive";
        public const string LivenessEndpointPath = "/liveness";
        public const string ReadynessEndpointPath = "/readiness";
        public const string QuartzDashboardEndpointPath = "/quartz";

        public static readonly FrozenSet<string> ExcludedPaths = new[]
        {
            HealthEndpointPath,
            HealthZEndpointPath,
            AlivenessEndpointPath,
            LivenessEndpointPath,
            ReadynessEndpointPath
        }
        .ToFrozenSet(StringComparer.OrdinalIgnoreCase);

        public static bool IsExcludedPath(PathString path)
        {
            foreach (var excluded in ExcludedPaths)
            {
                if (path.StartsWithSegments(excluded))
                    return true;
            }

            return false;
        }
    }

    public static class Schemes
    {
        public static readonly string HttpOrHttps = $"{Uri.UriSchemeHttps}+{Uri.UriSchemeHttp}";
    }

    public static class ContextEnrich
    {
        public const string DomainName = nameof(DomainName);
        public const string TenantName = nameof(TenantName);
        public const string TenantTier = nameof(TenantTier);
    }
}
