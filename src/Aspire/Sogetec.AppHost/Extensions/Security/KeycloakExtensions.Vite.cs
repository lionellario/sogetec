using Aspire.Hosting.JavaScript;

namespace Sogetec.AppHost.Extensions.Security;

internal static partial class KeycloakExtensions
{
    extension(IResourceBuilder<ViteAppResource> builder)
    {
        /// <summary>
        ///     Configures the Vite app resource to integrate with Keycloak as an Identity Provider (IdP).
        /// </summary>
        /// <param name="keycloak">The Keycloak resource builder to configure as an IdP.</param>
        /// <returns>The Vite app resource builder for method chaining.</returns>
        public IResourceBuilder<ViteAppResource> WithKeycloak(IResourceBuilder<KeycloakResource> keycloak)
        {
            var clientId = builder.Resource.Name;
            ConfigureKeycloakForClient(
                keycloak,
                builder,
                clientId,
                "APP",
                null,
                false
            );

            builder
                .WithReference(keycloak)
                .WaitForStart(keycloak)
                .WithEnvironment("VITE_KEYCLOAK_HTTP", keycloak.GetEndpoint(Uri.UriSchemeHttp))
                .WithEnvironment("VITE_KEYCLOAK_REALM", _defaultLocalKeycloakName)
                .WithEnvironment("VITE_KEYCLOAK_CLIENT_ID", clientId);

            return builder;
        }
    }
}
