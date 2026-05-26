namespace Sogetec.Chassis.Security.Keycloak;

internal sealed class KeycloakRolesClaimsTransformation(
    ILogger<KeycloakRolesClaimsTransformation> logger
) : IClaimsTransformation
{
    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (
            !principal.TryGetJsonClaim("realm_access", out var realmAccess)
            || realmAccess["roles"] is not JsonArray realmRoles
        )
        {
            return principal;
        }

        var userId = principal.GetAuthenticatedUserId();

        var claimsIdentity = new ClaimsIdentity();

        // Convert realm roles to regular roles.
        var userRoles = realmRoles.GetValues<string>().ToList();

        if (userRoles is null || userRoles.Count == 0)
        {
            logger.LogWarning("AUTHENTICATION_FAILED. User={userId} doesn't have roles.", userId);
            throw new InvalidOperationException("User Role is not set on claim principal");
        }

        userRoles = [.. userRoles.Select(x => x.Trim().ToLowerInvariant()).OrderBy(x => x)];

        // var roleKey = string.Join("|", userRoles);
        // var rolesHash = ComputeHash(roleKey);

        // var permissions = await cache.GetOrSetAsync<List<string>>(
        //     key: CacheKey.ForPermissions(tenantId, rolesHash),
        //     factory: async (ctx, cancellationToken) =>
        //     {
        //         var permissions = await permissionRepository.GetUserPermissionsAsync(
        //             userRoles,
        //             cancellationToken);

        //         if (permissions.Count == 0)
        //         {
        //             ctx.Options.SetFrom(_cacheForUserPermissionsEmpty);
        //         }
        //         else
        //         {
        //             ctx.Options.SetFrom(_cacheForUserPermissions);
        //         }

        //         return [.. permissions];
        //     },
        //     tags: [CacheTag.UserPermissions]);

        // if (permissions.Count == 0)
        // {
        //     logger.LogWarning("AUTHENTICATION_FAILED. User={userId} doesn't have permissions.", userId);
        //     return principal;
        // }

        var roles = userRoles.Select(r => new Claim(ClaimTypes.Role, r));

        claimsIdentity.AddClaims(roles);

        principal.AddIdentity(claimsIdentity);

        logger.LogInformation(
            "ClaimPrincipal transformed and enriched with roles, " +
            "for userId={user}, role_count={rcount}",
            userId.ToString(), userRoles.Count
        );

        return principal;
    }
}
