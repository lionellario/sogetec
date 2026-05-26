using Sogetec.Constants.Aspire;

namespace Sogetec.AppHost.Extensions.Security;

internal static partial class KeycloakExtensions
{
    extension(IResourceBuilder<ProjectResource> builder)
    {
        /// <summary>
        ///     Configures the project resource to integrate with Keycloak as an Identity Provider (IdP).
        /// </summary>
        /// <param name="keycloak">The Keycloak resource builder to configure as an IdP.</param>
        /// <returns>The project resource builder for method chaining.</returns>
        public IResourceBuilder<ProjectResource> WithKeycloak(IResourceBuilder<KeycloakResource> keycloak)
        {
            var clientId = builder.Resource.Name;

            var clientSecret = builder.ApplicationBuilder
                            .AddParameter($"{clientId}-secret", true)
                            .WithGeneratedDefault(new() { MinLength = 32, Special = false });

            ConfigureKeycloakForClient(
                keycloak,
                builder,
                clientId,
                "API",
                clientSecret,
                true
            );

            builder
                .WithReference(keycloak)
                .WaitForStart(keycloak)
                .WithEnvironment("Identity__Realm", _defaultLocalKeycloakName)
                .WithEnvironment("Identity__ClientId", clientId)
                .WithEnvironment("Identity__ClientSecret", clientSecret);

            return builder;
        }
    }
}
