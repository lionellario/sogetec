using Sogetec.Chassis.Security.Keycloak.Services;

namespace Sogetec.Chassis.Security.Keycloak;

public static class KeycloakExtensions
{
    extension(IHostApplicationBuilder builder)
    {
        /// <summary>
        /// Get the roles from the claims, retrives the corresponding permissions from the database
        /// and register <see cref="CustomClaimType.Permission" /> claims.
        /// </summary>
        /// <returns>
        ///     The current <see cref="IHostApplicationBuilder" /> instance for fluent chaining.
        /// </returns>
        public IHostApplicationBuilder WithKeycloakClaimsTransformation()
        {
            builder.Services.AddTransient<IClaimsTransformation, KeycloakRolesClaimsTransformation>();
            return builder;
        }
    }

    extension(IServiceCollection services)
    {
        /// <summary>
        ///     Registers the Keycloak token introspection middleware in the dependency injection container.
        /// </summary>
        /// <returns>
        ///     The updated <see cref="IServiceCollection" /> instance.
        /// </returns>
        public IServiceCollection AddKeycloakTokenIntrospection()
        {
            return services.AddScoped<KeycloakTokenIntrospectionMiddleware>();
        }

        /// <summary>
        ///     Registers the Keycloak services in the dependency injection container.
        /// </summary>
        /// <returns>
        ///     The updated <see cref="IServiceCollection" /> instance.
        /// </returns>
        public IServiceCollection AddKeycloakServices()
        {
            services.AddScoped<IKeycloakTokenProvider, KeycloakTokenProvider>();
            services.AddScoped<IKeycloakAdminClient, KeycloakAdminClient>();
            return services;
        }
    }

    extension(IApplicationBuilder app)
    {
        /// <summary>
        ///     Adds the Keycloak token introspection middleware to the application request pipeline.
        /// </summary>
        /// <returns>
        ///     The same <see cref="IApplicationBuilder" /> instance so additional middleware can be chained.
        /// </returns>
        public IApplicationBuilder UseKeycloakTokenIntrospection()
        {
            return app.UseMiddleware<KeycloakTokenIntrospectionMiddleware>();
        }
    }
}
