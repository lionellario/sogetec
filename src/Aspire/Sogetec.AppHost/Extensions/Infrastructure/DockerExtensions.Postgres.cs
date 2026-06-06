using Sogetec.Constants.Aspire;

namespace Sogetec.AppHost.Extensions.Infrastructure;

public sealed class ApplicationDatabases
{
    public required IResourceBuilder<IResourceWithConnectionString> Sogetec { get; init; }
}

internal static partial class DockerExtensions
{
    extension(IDistributedApplicationBuilder builder)
    {
        public IResourceBuilder<PostgresServerResource> AddPostgresServer(string name)
        {
            if (builder.ExecutionContext.IsRunMode)
            {
                var postgres = builder
                    .AddPostgres(name)
                    .WithIconName("SogetecDatabase")
                    .WithPgAdmin(o =>
                    {
                        o.WithImagePullPolicy(ImagePullPolicy.Always)
                        .WithHostPort(5050)
                        .WithLifetime(ContainerLifetime.Persistent);
                    })
                    .WithImageTag("18.3")
                    .WithImagePullPolicy(ImagePullPolicy.Always)
                    .WithLifetime(ContainerLifetime.Persistent)
                    // Issue: https://github.com/dotnet/aspire/issues/11710
                    .WithVolume("Sogetec_pg_data", "/var/lib/postgresql/18/docker")
                    .WithHostPort(5432);

                return postgres;
            }

            // In production these come from environment variables on the VPS.
            return builder
                    .AddPostgres(name)
                    .PublishAsConnectionString();
        }

        /// <summary>
        /// Adds all PostgreSQL database to the application model.
        /// </summary>
        /// <returns>A reference to the <see cref="ApplicationDatabases"/>.</returns>
        public ApplicationDatabases AddApplicationDatabases()
        {
            if (builder.ExecutionContext.IsRunMode)
            {
                var postgres = builder.Resources
                    .OfType<PostgresServerResource>()
                    .Single();

                var postgresBuilder = builder.CreateResourceBuilder(postgres);

                return new ApplicationDatabases
                {
                    Sogetec = postgresBuilder.AddDatabase(Components.DatabaseName.Sogetec)
                };
            }

            //
            // PRODUCTION
            //
            // External providers inject connection strings
            //
            // Example:
            // ConnectionStrings__accounting
            // ConnectionStrings__catalog
            // etc.
            //

            return new ApplicationDatabases
            {
                Sogetec = builder.AddConnectionString(Components.DatabaseName.Sogetec)
            };
        }
    }
}
