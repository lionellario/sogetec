using Sogetec.Chassis.OpenTelemetry.ActivityScope;
using Sogetec.Constants.Core;

namespace Sogetec.ServiceDefaults;

// Adds common .NET Aspire services: service discovery, resilience, health checks, and OpenTelemetry.
// This project should be referenced by each service project in your solution.
// To learn more about using this project, see https://aka.ms/dotnet/aspire/service-defaults
public static class Extensions
{
    extension(WebApplication app)
    {
        /// <summary>
        ///     Maps development-only health check endpoints for readiness and liveness probing.
        /// </summary>
        /// <remarks>
        ///     The readiness endpoint requires all checks to pass, while the liveness endpoint
        ///     evaluates only checks tagged with <c>live</c>.
        /// </remarks>
        public void MapDefaultEndpoints()
        {
            if (!app.Environment.IsDevelopment())
            {
                return;
            }

            // All health checks must pass for app to be considered ready to accept traffic after starting.
            app.MapHealthChecks(Http.Endpoints.HealthEndpointPath);

            // Only health checks tagged with the "live" tag must pass for app to be considered alive.
            app.MapHealthChecks(
                Http.Endpoints.AlivenessEndpointPath,
                new() { Predicate = r => r.Tags.Contains("live") }
            );
        }
    }

    extension(IHostApplicationBuilder builder)
    {
        /// <summary>
        ///     Configures the default platform capabilities for a service host.
        /// </summary>
        /// <remarks>
        ///     This enables OpenTelemetry, baseline health checks, service discovery, and
        ///     default HTTP client resilience/service discovery behavior.
        /// </remarks>
        public void AddServiceDefaults()
        {
            builder.ConfigureOpenTelemetry();

            builder.AddDefaultHealthChecks();

            builder.Services.AddServiceDiscovery();

            builder.Services.ConfigureHttpClientDefaults(http =>
            {
                http.RemoveAllResilienceHandlers();

                // Turn on resilience by default
                http.AddStandardResilienceHandler();

                // Turn on service discovery by default
                http.AddServiceDiscovery();
            });
        }

        private void ConfigureOpenTelemetry()
        {
            builder.Services.AddHttpContextAccessor();

            builder.AddLogging();

            builder.AddOpenTelemetry();

            builder.AddOpenTelemetryExporters();
        }

        private void AddDefaultHealthChecks()
        {
            builder
                .Services.AddHealthChecks()
                // Add a default liveness check to ensure app is responsive
                .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);
        }

        private void AddLogging()
        {
            var logger = builder.Logging;
            var loggerSection = builder.Configuration.GetSection("Logging");

            builder.Logging.ClearProviders();
            logger.AddGlobalBuffer(loggerSection);
            logger.AddPerIncomingRequestBuffer(loggerSection);

            logger.AddOpenTelemetry(logging =>
            {
                logging.IncludeFormattedMessage = true;
                logging.IncludeScopes = true;
            });

            if (builder.Environment.IsDevelopment())
            {
                logger.AddTraceBasedSampler();
            }
            else
            {
                logger.AddRandomProbabilisticSampler(loggerSection);
            }
        }

        private void AddOpenTelemetry()
        {
            Activity.DefaultIdFormat = ActivityIdFormat.W3C;

            builder
                .Services
                .AddOpenTelemetry()
                .WithMetrics(metrics =>
                {
                    metrics
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddRuntimeInstrumentation()
                        .AddMeter(ActivitySourceProvider.DefaultSourceName, "Microsoft.EntityFrameworkCore");
                })
                .WithTracing(tracing =>
                {
                    if (builder.Environment.IsDevelopment())
                    {
                        tracing.SetSampler(new AlwaysOnSampler());
                    }

                    tracing
                        .AddSource(builder.Environment.ApplicationName)
                        .AddAspNetCoreInstrumentation(options =>
                        {
                            // Don't trace requests to the health endpoint to avoid filling the dashboard with noise
                            options.Filter = ctx => !Http.Endpoints.IsExcludedPath(ctx.Request.Path);
                            options.RecordException = true;
                        })
                        .AddEntityFrameworkCoreInstrumentation(options =>
                        {
                            // Enrich spans with additional context
                            options.EnrichWithIDbCommand = (activity, command) =>
                            {
                                // Add the database name as a tag
                                var dbName = command.Connection?.Database;
                                var displayname = dbName;
                                var sqlcmd = command.CommandText;

                                if (sqlcmd.StartsWith("select", StringComparison.OrdinalIgnoreCase))
                                {
                                    displayname = "SELECT";
                                }

                                if (sqlcmd.StartsWith("insert", StringComparison.OrdinalIgnoreCase))
                                {
                                    displayname = "INSERT";
                                }

                                if (sqlcmd.StartsWith("delete", StringComparison.OrdinalIgnoreCase))
                                {
                                    displayname = "DELETE";
                                }

                                if (sqlcmd.StartsWith("update", StringComparison.OrdinalIgnoreCase))
                                {
                                    displayname = "UPDATE";
                                }

                                activity.DisplayName = displayname!;

                                activity.SetTag("db.name", dbName);
                                activity.SetTag("db.command_type", command.CommandType);

                                if (builder.Environment.IsDevelopment())
                                {
                                    activity.SetTag("db.sql", sqlcmd);
                                }

                                // Add parameter count (not values, to avoid sensitive data)
                                activity.SetTag("db.parameter_count", command.Parameters.Count);
                            };

                            options.Filter = (activityName, command) =>
                            {
                                // Skip health check queries
                                if (command.CommandText?.Contains("SELECT 1") == true)
                                    return false;

                                // Skip migration queries
                                if (command.CommandText?.Contains("__EFMigrationsHistory") == true)
                                    return false;

                                return true;
                            };
                        })
                        //.AddGrpcClientInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddFixHttpRouteProcessor()
                        .AddSource(ActivitySourceProvider.DefaultSourceName);
                });
        }

        private void AddOpenTelemetryExporters()
        {
            var useOtlpExporter = !string.IsNullOrWhiteSpace(builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]);

            if (useOtlpExporter)
            {
                builder.Services.AddOpenTelemetry().UseOtlpExporter();
            }
        }
    }
}
