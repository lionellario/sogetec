using Sogetec.Constants.Aspire;

namespace Sogetec.AppHost.Extensions.Security;

internal static partial class KeycloakExtensions
{
    private const string ThemeName = "THEME_NAME";
    private const string RealmName = "REALM_NAME";
    private const string RealmDisplayName = "REALM_DISPLAY_NAME";
    private const string AdminUserName = "ADMIN_USER_NAME";
    private const string AdminUserPassword = "ADMIN_USER_PASSWORD";
    private const string HttpEnabledEnvVarName = "KC_HTTP_ENABLED";
    private const string ProxyHeadersEnvVarName = "KC_PROXY_HEADERS";
    private const string HostNameStrictEnvVarName = "KC_HOSTNAME_STRICT";
    private const string BaseContainerPath = "Container/Keycloak";
    private static readonly string _defaultLocalKeycloakName = nameof(Sogetec).ToLowerInvariant();

    private static void ConfigureKeycloakForClient<TResource>(
        IResourceBuilder<KeycloakResource> keycloakContainer,
        IResourceBuilder<TResource> clientBuilder,
        string clientId,
        string clientType,
        IResourceBuilder<ParameterResource>? clientSecret,
        bool includeContainerHostUrl
    )
        where TResource : IResourceWithEndpoints
    {
        var clientEnv = clientId.ToUpperInvariant().Replace("-", "_");

        keycloakContainer
            .WithEnvironment(HttpEnabledEnvVarName, "true")
            .WithEnvironment(ProxyHeadersEnvVarName, "xforwarded")
            .WithEnvironment(HostNameStrictEnvVarName, "false")
            .WithEnvironment($"CLIENT_{clientEnv}_ID", clientId)
            .WithEnvironment(
                $"CLIENT_{clientEnv}_NAME",
                Services.ToClientName(clientId, clientType)
            );

        if (clientSecret is not null)
        {
            keycloakContainer.WithEnvironment($"CLIENT_{clientEnv}_SECRET", clientSecret);
        }

        keycloakContainer.OnResourceEndpointsAllocated(
            (resource, _, _) =>
            {
                var resourceBuilder = clientBuilder.ApplicationBuilder.CreateResourceBuilder(
                    resource
                );

                resourceBuilder.WithEnvironment(context =>
                {
                    var endpoint = clientBuilder.GetEndpoint(Uri.UriSchemeHttp);

                    context.EnvironmentVariables.Add($"CLIENT_{clientEnv}_URL", endpoint.Url);

                    if (includeContainerHostUrl)
                    {
                        context.EnvironmentVariables.Add(
                            $"CLIENT_{clientEnv}_URL_CONTAINERHOST",
                            endpoint
                        );
                    }
                });

                return Task.CompletedTask;
            }
        );
    }
}
