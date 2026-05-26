using Microsoft.AspNetCore.Authorization;

namespace Sogetec.Chassis.Security.Authorization;

public sealed class AuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private static PermissionRequirement? AuthorizeUser(ClaimsPrincipal user, PermissionRequirement requirement)
    {
        if (user?.Identity is null)
        {
            return null;
        }

        if (!user.Identity.IsAuthenticated)
        {
            return null;
        }

        var userPerms = user.FindAll(CustomClaimType.Permission)?.Select(x => x.Value) ?? [];

        if (!userPerms.Any())
        {
            return null;
        }

        var authorized = userPerms.Any(x => x.Equals(requirement.Permission));

        return authorized ? requirement : null;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var authorized = AuthorizeUser(context.User, requirement);

        if (authorized is not null)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }

        return Task.CompletedTask;
    }
}
