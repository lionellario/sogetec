using Aspire.Hosting.Yarp;
using Aspire.Hosting.Yarp.Transforms;
using Sogetec.Constants.Aspire;

namespace Sogetec.AppHost.Extensions.Security;

internal static partial class KeycloakExtensions
{
    extension(IYarpConfigurationBuilder builder)
    {
        /// <summary>
        ///     Configures the project resource to integrate with Keycloak as an Identity Provider (IdP).
        /// </summary>
        /// <param name="keycloak">The Keycloak resource builder to configure as an IdP.</param>
        /// <returns>The project resource builder for method chaining.</returns>
        public YarpRoute WithKeycloak(IResourceBuilder<IResource> keycloak)
        {
            switch (keycloak)
            {
                case IResourceBuilder<KeycloakResource> keycloakContainer:
                    {
                        return builder
                            .AddRoute(keycloakContainer)
                            .WithMatchHosts(["identity.Sogetec.com"])
                            .WithTransformForwarded()
                            .WithMaxRequestBodySize(10 * 1024 * 1024)
                            .WithTransformRequestHeader("X-Forwarded-Proto", "https", append: false)
                            .WithTransformResponseHeader("X-Powered-By", $"{nameof(Sogetec)} {nameof(Services.Gateway)}");
                    }
                case IResourceBuilder<ExternalServiceResource> keycloakHosted:
                    {
                        return builder
                            .AddRoute(keycloakHosted)
                            .WithMatchHosts(["identity.Sogetec.com"])
                            .WithTransformForwarded()
                            .WithMaxRequestBodySize(10 * 1024 * 1024)
                            .WithTransformRequestHeader("X-Forwarded-Proto", "https", append: false)
                            .WithTransformResponseHeader("X-Powered-By", $"{nameof(Sogetec)} {nameof(Services.Gateway)}");
                    }
                default:
                    throw new NotSupportedException($"The type {keycloak.GetType().Name} is not supported.");
            }

        }
    }
}
