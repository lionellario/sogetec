using Sogetec.Chassis.Security.Keycloak.Models;

namespace Sogetec.Chassis.Security.Keycloak.Services;

public sealed class KeycloakTokenProvider(
    IHttpClientFactory httpClientFactory,
    IFusionCache cache,
    IdentityOptions identityOptions,
    ILogger<KeycloakTokenProvider> logger) : IKeycloakTokenProvider
{
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        TypeInfoResolver = KeycloakJsonContext.Default,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public async Task<string> GetAccessTokenAsync(CancellationToken cancellationToken = default)
    {
        var cacheKey = CacheKey.ForAccessToken();

        var accessToken = await cache.GetOrSetAsync<string>(
            key: cacheKey,
            factory: async (ctx, ct) =>
            {
                var endpoint = KeycloakEndpoints
                .Token(identityOptions.Realm)
                .TrimStart('/');

                using var httpClient = httpClientFactory.CreateClient(Components.KeyCloak);

                var requestContent = new Dictionary<string, string>
                {
                    ["grant_type"] = "client_credentials",
                    ["client_id"] = identityOptions.ClientId,
                    ["client_secret"] = identityOptions.ClientSecret,
                    ["scope"] = "openid"
                };

                var token = await httpClient.SendAsync<TokenRepresentation>(
                    method: HttpMethod.Post,
                    url: endpoint,
                    body: requestContent,
                    mediaType: "application/x-www-form-urlencoded",
                    jsonOptions: _jsonOptions,
                    cancellationToken: cancellationToken
                );

                if (token is null || string.IsNullOrWhiteSpace(token.AccessToken))
                {
                    logger.LogError("Failed to retrieve access token from Keycloak");
                    throw new InvalidOperationException("Failed to retrieve access token from Keycloak");
                }

                ctx.Options.Duration = TimeSpan.FromSeconds(token.ExpiresIn - 30);
                return token.AccessToken;
            },
            token: cancellationToken);

        return accessToken;
    }
}
