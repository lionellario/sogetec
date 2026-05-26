namespace Sogetec.Chassis.Security.Extensions;

public static class AuthenticationExtensions
{
    extension(IHostApplicationBuilder builder)
    {
        private static Task OnTokenValidatedAsync(TokenValidatedContext context)
        {
            var token = context.SecurityToken switch
            {
                JsonWebToken jwt => jwt.EncodedToken,
                JwtSecurityToken jwst => jwst.RawData,
                _ => string.Empty
            };

            if (context is { Principal.Identity: ClaimsIdentity identity })
            {
                identity.AddClaim(new(CustomClaimType.AccessToken, token));
            }

            return Task.CompletedTask;
        }

        /// <summary>
        ///     Configures the default JWT bearer authentication pipeline using Keycloak settings
        ///     resolved from the configured identity options.
        /// </summary>
        /// <remarks>
        ///     This method also registers a named HTTP client for Keycloak and applies stricter
        ///     token validation outside development environments.
        /// </remarks>
        /// <returns>The same <see cref="IHostApplicationBuilder" /> instance for fluent configuration.</returns>
        public IHostApplicationBuilder AddDefaultAuthentication()
        {
            var services = builder.Services;

            // Binds identity configuration section to <see cref="IdentityOptions"/>.
            builder.Configure<IdentityOptions>(IdentityOptions.ConfigurationSection);

            // Resolves the Keycloak realm from bound identity options.
            var realm = services.BuildServiceProvider().GetRequiredService<IdentityOptions>().Realm;

            // Uses HTTP in development and HTTP/HTTPS for non-development environments.
            var scheme = builder.Environment.IsDevelopment()
                ? Uri.UriSchemeHttp
                : Http.Schemes.HttpOrHttps;

            var keycloakUrl = HttpUtilities
                .AsUrlBuilder()
                .WithScheme(scheme)
                .WithHost(Components.KeyCloak)
                .Build();

            if (builder.Environment.IsDevelopment())
            {
                keycloakUrl = builder.Configuration["KEYCLOAK_HTTP"] ?? string.Empty;
            }

            // Registers a named HTTP client used for Keycloak communication.
            services.AddHttpClient(
                Components.KeyCloak,
                client => client.BaseAddress = new(keycloakUrl)
            );

            // Configures JWT bearer authentication backed by Keycloak.
            builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddKeycloakJwtBearer(
                    Components.KeyCloak,
                    realm,
                    options =>
                    {
                        options.Audience = "account";
                        options.RequireHttpsMetadata = !builder.Environment.IsDevelopment();
                        options.TokenValidationParameters.ValidateAudience = true;
                        options.TokenValidationParameters.ValidateIssuer = !builder.Environment.IsDevelopment();
                        options.TokenValidationParameters.ClockSkew = TimeSpan.Zero;

                        options.Events = new JwtBearerEvents
                        {
                            OnAuthenticationFailed = context =>
                            {
                                Console.WriteLine(context.Exception);
                                return Task.CompletedTask;
                            },
                            OnTokenValidated = context => OnTokenValidatedAsync(context)
                        };
                    }
                );

            return builder;
        }
    }
}
