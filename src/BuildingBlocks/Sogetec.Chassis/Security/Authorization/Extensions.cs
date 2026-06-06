using Microsoft.AspNetCore.Authorization;
using Sogetec.Chassis.Security.Keycloak;

namespace Sogetec.Chassis.Security.Authorization;

public static class AuthorizationExtensions
{
    extension(IHostApplicationBuilder builder)
    {
        /// <summary>
        ///     Configures the default authorization pipeline.
        ///     This uses a custom validation to check for specific permissions to access API endpoints.
        ///     This is triggered by the .RequireAuthorization({permission_name}) on an endpoint.
        ///     The custom authorization will check for the permission_name in the ClaimPrincipal or Authenticated user,
        ///     populated <see cref="KeycloakRolesClaimsTransformation" />.
        /// </summary>
        /// <returns>The same <see cref="IHostApplicationBuilder" /> instance for fluent configuration.</returns>
        public IHostApplicationBuilder AddDefaultAuthorization()
        {
            builder.Services
                .AddAuthorizationBuilder()
                .AddPolicy(
                    Authorize.Policies.Admin,
                    policy =>
                    {
                        policy
                            .RequireAuthenticatedUser()
                            .RequireRole(Authorize.Roles.Admin);
                    }
                )
                .AddPolicy(
                    Authorize.Policies.User,
                    policy =>
                    {
                        policy
                            .RequireAuthenticatedUser()
                            .RequireRole(Authorize.Roles.User);
                    }
                )
                .SetDefaultPolicy(
                    new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build()
                );
            return builder;
        }
    }
}
