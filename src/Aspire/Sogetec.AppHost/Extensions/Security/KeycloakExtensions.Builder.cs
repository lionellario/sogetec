using Sogetec.Constants.Aspire;
namespace Sogetec.AppHost.Extensions.Security;

internal static partial class KeycloakExtensions
{
    extension(IDistributedApplicationBuilder builder)
    {
        /// <summary>
        ///     Adds a Keycloak container resource to the distributed application builder with custom theme and realm import
        ///     settings.
        /// </summary>
        /// <param name="name">The name of the Keycloak resource.</param>
        /// <returns>
        ///     An <see cref="IResourceBuilder{KeycloakResource}" /> representing the configured Keycloak resource.
        /// </returns>
        public IResourceBuilder<KeycloakResource> AddKeycloakServer(string name)
        {
            var keycloak = builder
                .AddKeycloak(name)
                .WithDataVolume()
                .WithOtlpExporter()
                .WithArgs("--health-enabled=true")
                .WithIconName("LockClosedRibbon")
                .WithImagePullPolicy(ImagePullPolicy.Always)
                .WithLifetime(ContainerLifetime.Persistent)
                .WithExternalHttpEndpoints()
                .WithSampleRealmImport(_defaultLocalKeycloakName, nameof(Sogetec));
            // .WithCustomTheme(_defaultLocalKeycloakName)

            if (builder.ExecutionContext.IsPublishMode)
            {
                var dbname = builder.AddParameter("kc-db-name", true)
                    .WithDescription(ParameterDescriptions.Keycloak.DatabaseName, true)
                    .WithCustomInput(_ =>
                        new()
                        {
                            Name = "KeycloakDatabaseNameParameter",
                            Label = "Keycloak Database Name",
                            InputType = InputType.Text,
                            Value = "keycloak",
                            Description = "Enter the name of the Keycloak database to use (must match the database name configured for the Keycloak server)",
                        }
                    );

                var dburl = builder.AddParameter("kc-db-url", true)
                    .WithDescription(ParameterDescriptions.Keycloak.DatabaseUrl, true)
                    .WithCustomInput(_ =>
                        new()
                        {
                            Name = "KeycloakDatabaseUrlParameter",
                            Label = "Keycloak Database URL",
                            InputType = InputType.Text,
                            Value = "jdbc:postgresql://postgres:5432/keycloak",
                            Description = "Enter the JDBC URL for the Keycloak database (must match the connection string configured for the Keycloak server)",
                        }
                    );

                var dbuser = builder.AddParameter("kc-db-user", true)
                    .WithDescription(ParameterDescriptions.Keycloak.DatabaseUsername, true)
                    .WithCustomInput(_ =>
                        new()
                        {
                            Name = "KeycloakDatabaseUserParameter",
                            Label = "Keycloak Database User",
                            InputType = InputType.Text,
                            Value = "keycloak",
                            Description = "Enter the username for the Keycloak database (must match the username configured for the Keycloak server)",
                        }
                    );

                var dbpassword = builder.AddParameter("kc-db-password", true)
                    .WithDescription(ParameterDescriptions.Keycloak.DatabasePassword, true)
                    .WithCustomInput(_ =>
                        new()
                        {
                            Name = "KeycloakDatabasePasswordParameter",
                            Label = "Keycloak Database Password",
                            InputType = InputType.Text,
                            Value = "keycloak",
                            Description = "Enter the password for the Keycloak database (must match the password configured for the Keycloak server)",
                        }
                    );

                var keycloakUrl = builder
                    .AddParameter("kc-url", true)
                    .WithDescription(ParameterDescriptions.Keycloak.Url, true)
                    .WithCustomInput(_ =>
                        new()
                        {
                            Name = "KeycloakUrlParameter",
                            Label = "Keycloak URL",
                            InputType = InputType.Text,
                            Value = "https://identity.Sogetec.com",
                            Description = "Enter your Keycloak server URL here (must be https)",
                        }
                    );

                keycloak
                    .WithArgs(
                        $"--db={dbname}",
                        $"--db-url={dburl}",
                        $"--db-username={dbuser}",
                        $"--db-password={dbpassword}",
                        $"--hostname={keycloakUrl}",
                        "--hostname-protocol=https",
                        "--https-certificate-file=/opt/keycloak/certs/tls.crt",
                        "--https-certificate-key-file=/opt/keycloak/certs/tls.key",
                        "--proxy=edge",
                        "--features=token-exchange,admin-fine-grained-authz",
                        "--spi-events-listener-jboss-logging-success-level=info",
                        "--spi-events-listener-jboss-logging-error-level=warn"
                    )
                    .WithBindMount($"{BaseContainerPath}/Realms", "/opt/keycloak/realm-import/", true);

                keycloakUrl.WithParentRelationship(keycloak);
            }

            return keycloak;
        }
    }

    extension(IResourceBuilder<KeycloakResource> builder)
    {
        private IResourceBuilder<KeycloakResource> WithSampleRealmImport(string realmName, string displayName)
        {
            builder
                .WithRealmImport($"{BaseContainerPath}/Realms")
                .WithEnvironment(RealmName, realmName)
                .WithEnvironment(RealmDisplayName, displayName);

            return builder;
        }

        private IResourceBuilder<KeycloakResource> WithCustomTheme(string themeName)
        {
            var importFullPath = Path.GetFullPath(
                $"{BaseContainerPath}/themes",
                builder.ApplicationBuilder.AppHostDirectory
            );

            if (Directory.Exists(importFullPath))
            {
                builder
                    .WithBindMount(importFullPath, "/opt/keycloak/providers/", true)
                    .WithEnvironment(ThemeName, themeName);
            }

            return builder;
        }
    }
}
