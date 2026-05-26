using System.Collections.Concurrent;
using Microsoft.AspNetCore.Authorization;

namespace Sogetec.Chassis.Security.Authorization;

public class PermissionAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
    : DefaultAuthorizationPolicyProvider(options)
{
    private readonly ConcurrentDictionary<string, AuthorizationPolicy> _policyCache = new();

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        if (_policyCache.TryGetValue(policyName, out var lCachedPolicy))
        {
            return lCachedPolicy;
        }

        var policy = await base.GetPolicyAsync(policyName);

        if (policy != null)
        {
            return policy;
        }

        var newPolicy = new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .AddRequirements(new PermissionRequirement(policyName))
                            .Build();
        _policyCache.TryAdd(policyName, newPolicy);

        return newPolicy;
    }
}
