using Aspire.Hosting.Yarp;
using Aspire.Hosting.Yarp.Transforms;
using Projects;
using Sogetec.AppHost.Extensions.Infrastructure;
using Sogetec.AppHost.Extensions.Network;
using Sogetec.AppHost.Extensions.Security;
using Sogetec.Constants.Aspire;
using Sogetec.Constants.Core;
using Yarp.ReverseProxy.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

// Publish this as a Docker Compose application
builder
    .AddDockerComposeEnvironment("env")
    .WithDashboard(db => db.WithHostPort(8085))
    .ConfigureComposeFile(file =>
    {
        file.Name = "sogetec";
    });


// ------------------------------------------------------------
// Infrastructure
// ------------------------------------------------------------
var sqlite = builder.AddSqlite(Components.Sqlite, "Modules/Data", "sogeteclite.db");
// var postgres = builder.AddPostgresServer(Components.Postgres);
// var databases = builder.AddApplicationDatabases();
var redis = builder.AddRedisServer(Components.Redis);
var keycloak = builder.AddKeycloakServer(Components.KeyCloak);

// ------------------------------------------------------------
// Backend API
// ------------------------------------------------------------

var api = builder
    .AddProject<Api>(Services.WebApi)
    .WithReference(redis)
    .WithReference(sqlite)
    // .WithReference(databases.Sogetec)
    // .WaitFor(databases.Sogetec)
    .WaitFor(redis)
    .WaitFor(sqlite)
    .WithKeycloak(keycloak)
    .WithFriendlyUrls();

// ------------------------------------------------------------
// Frontends
// ------------------------------------------------------------

var spa = builder
    .AddViteApp(Services.SPA, "../../Clients/frontend")
    .WithOtlpExporter()
    .WithHttpsEndpoint(port: 3000, env: "PORT")
    .WithExternalHttpEndpoints()
    .WithHttpsDeveloperCertificate()
    .WithReference(api)
    .WithEnvironment("VITE_SOGETEC_API_HTTPS", api.GetEndpoint(Uri.UriSchemeHttps))
    .WithEnvironment("VITE_SOGETEC_API_HTTP", api.GetEndpoint(Uri.UriSchemeHttp))
    .WaitFor(api)
    .WithKeycloak(keycloak)
    .WithFriendlyUrls()
    .PublishAsDockerFile();

var admin = builder
    .AddViteApp(Services.ADMIN, "../../Clients/admin")
    .WithOtlpExporter()
    .WithHttpsEndpoint(port: 3001, env: "PORT")
    .WithExternalHttpEndpoints()
    .WithHttpsDeveloperCertificate()
    .WithReference(api)
    .WithEnvironment("VITE_SOGETEC_API_HTTPS", api.GetEndpoint(Uri.UriSchemeHttps))
    .WithEnvironment("VITE_SOGETEC_API_HTTP", api.GetEndpoint(Uri.UriSchemeHttp))
    .WaitFor(api)
    .WithKeycloak(keycloak)
    .WithFriendlyUrls()
    .PublishAsDockerFile();

// ------------------------------------------------------------
// Gateway
// ------------------------------------------------------------

builder
    .AddYarp(Services.Gateway)
    .WithHttpsDeveloperCertificate()
    .WithIconName("SerialPort")
    .WithHttpEndpoint(port: 80, name: "http")
    .WithHttpsEndpoint(port: 443, name: "https")
    .WithConfiguration(yarpBuilder =>
    {
        var defaultMaxRequestBodySize = 10 * 1024 * 1024;

        var defaultHealthCheckConfig = new HealthCheckConfig
        {
            Active = new()
            {
                Enabled = true,
                Interval = TimeSpan.FromSeconds(10),
                Timeout = TimeSpan.FromSeconds(5),
                Policy = "ConsecutiveFailures",
                Path = Http.Endpoints.HealthEndpointPath,
            }
        };

        var apiCluster = yarpBuilder
            .AddCluster(api)
            .WithLoadBalancingPolicy("RoundRobin")
            .WithHealthCheckConfig(defaultHealthCheckConfig);

        var spaCluster = yarpBuilder
            .AddCluster(spa)
            .WithLoadBalancingPolicy("RoundRobin")
            .WithHealthCheckConfig(defaultHealthCheckConfig);

        var adminCluster = yarpBuilder
            .AddCluster(admin)
            .WithLoadBalancingPolicy("RoundRobin")
            .WithHealthCheckConfig(defaultHealthCheckConfig);

        yarpBuilder
            .AddRoute(apiCluster)
            .WithMatchHosts(["api.sogetecsarl.com"])
            .WithTransformForwarded()
            .WithMaxRequestBodySize(defaultMaxRequestBodySize)
            .WithTransformRequestHeader("X-Forwarded-Proto", "https", append: false)
            .WithTransformRequestHeader("X-Real-IP", "{RemoteIpAddress}")
            .WithTransformResponseHeader("X-Powered-By", $"{nameof(Sogetec)} {nameof(Services.Gateway)}");

        yarpBuilder
            .AddRoute(adminCluster)
            .WithMatchHosts(["admin.sogetecsarl.com"])
            .WithTransformForwarded()
            .WithMaxRequestBodySize(defaultMaxRequestBodySize)
            .WithTransformRequestHeader("X-Forwarded-Proto", "https", append: false)
            .WithTransformRequestHeader("X-Real-IP", "{RemoteIpAddress}")
            .WithTransformResponseHeader("X-Powered-By", $"{nameof(Sogetec)} {nameof(Services.Gateway)}");

        yarpBuilder
            .AddRoute(spaCluster)
            .WithMatchHosts(["sogetecsarl.com"])
            .WithTransformForwarded()
            .WithMaxRequestBodySize(defaultMaxRequestBodySize)
            .WithOrder(100)
            .WithTransformRequestHeader("X-Forwarded-Proto", "https", append: false)
            .WithTransformRequestHeader("X-Forwarded-Host", "{Host}")
            .WithTransformResponseHeader("X-Powered-By", $"{nameof(Sogetec)} {nameof(Services.Gateway)}");

        yarpBuilder.WithKeycloak(keycloak);

    })
    .WithEnvironment("ASPNETCORE_FORWARDEDHEADERS_ENABLED", "true")
    .WithExplicitStart();

// ------------------------------------------------------------
// CORS
// ------------------------------------------------------------
if (builder.ExecutionContext.IsPublishMode)
{
    var (frontendUrl, adminUrl) = builder.AddCorsOriginParameters();
    api.WithCorsOrigins(frontendUrl, adminUrl);
}

await builder.Build().RunAsync();