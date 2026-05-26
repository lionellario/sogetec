using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Sogetec.Constants.Aspire;
using Sogetec.Constants.Core;

namespace Sogetec.AppHost.Extensions.Network;

internal static class CorsExtensions
{
    private static readonly string[] _defaultHeaders =
    [
        HeaderNames.ContentType,
        HeaderNames.Authorization,
        HeaderNames.Accept,
        HeaderNames.Origin,
        HeaderNames.XRequestedWith,
        HeaderNames.XPoweredBy,
        Http.RequestIdHeader,
    ];

    private static readonly string[] _defaultMethods =
    [
        HttpMethods.Get,
        HttpMethods.Post,
        HttpMethods.Put,
        HttpMethods.Delete,
        HttpMethods.Patch,
        HttpMethods.Options,
    ];

    extension(IDistributedApplicationBuilder builder)
    {
        /// <summary>
        ///     Configures CORS origin parameters for the backend app application to allow cross-origin requests
        ///     from the frontend application in production.
        /// </summary>
        /// <returns>
        ///     The SPA frontend URL parameter resource builders.
        /// </returns>
        public (
            IResourceBuilder<ParameterResource> frontendUrl,
            IResourceBuilder<ParameterResource> adminUrl
        ) AddCorsOriginParameters()
        {
            var frontend = builder
                .AddParameter("Sogetec-spa-url")
                .WithDescription(ParameterDescriptions.Cors.SogetecSpa, true)
                .WithCustomInput(_ =>
                    new()
                    {
                        Name = "SogetecSpaUrlParameter",
                        Label = "SogetecSpa URL",
                        InputType = InputType.Text,
                        Description =
                            "Enter the Sogetec-spa application URL for CORS (e.g., https://acme.Sogetec.com)",
                    }
                );

            var admin = builder
                .AddParameter("Sogetec-admin-url")
                .WithDescription(ParameterDescriptions.Cors.SogetecAdminSpa, true)
                .WithCustomInput(_ =>
                    new()
                    {
                        Name = "SogetecAdminUrlParameter",
                        Label = "SogetecAdmin URL",
                        InputType = InputType.Text,
                        Description =
                            "Enter the Sogetec-admin application URL for CORS (e.g., https://acme.Sogetec.com/admin)",
                    });

            return (frontend, admin);
        }
    }

    extension(IResourceBuilder<ProjectResource> builder)
    {
        /// <summary>
        ///     Applies CORS origin configuration to a backend service project, allowing cross-origin
        ///     requests from the specified Frontend URLs.
        /// </summary>
        /// <param name="SogetecSpaUrl">The Storefront URL parameter resource builder.</param>
        /// <returns>The original <see cref="IResourceBuilder{ProjectResource}" /> with CORS configuration applied.</returns>
        public IResourceBuilder<ProjectResource> WithCorsOrigins(
            IResourceBuilder<ParameterResource> frontendUrl,
            IResourceBuilder<ParameterResource> adminUrl
        )
        {
            builder
                .WithEnvironment("Cors__Origins__0", frontendUrl)
                .WithEnvironment("Cors__Origins__1", adminUrl)
                .WithEnvironment("Cors__AllowCredentials", "true");

            for (var i = 0; i < _defaultHeaders.Length; i++)
            {
                builder.WithEnvironment($"Cors__Headers__{i}", _defaultHeaders[i]);
            }

            for (var i = 0; i < _defaultMethods.Length; i++)
            {
                builder.WithEnvironment($"Cors__Methods__{i}", _defaultMethods[i]);
            }

            return builder;
        }
    }
}
