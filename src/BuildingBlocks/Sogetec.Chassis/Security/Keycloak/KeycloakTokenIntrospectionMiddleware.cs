using Microsoft.AspNetCore.Authorization;
using Sogetec.Chassis.Security.Enums;

namespace Sogetec.Chassis.Security.Keycloak;

internal sealed class KeycloakTokenIntrospectionMiddleware(
    IHttpClientFactory httpClientFactory,
    IdentityOptions identityOptions,
    ILogger<KeycloakTokenIntrospectionMiddleware> logger
) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var endpoint = context.GetEndpoint();

        var requiresAuth = endpoint?.Metadata.GetMetadata<IAuthorizeData>() is not null;
        var allowsAnonymous = endpoint?.Metadata.GetMetadata<IAllowAnonymous>() is not null;

        if (!requiresAuth || allowsAnonymous)
        {
            await next(context);
            return;
        }

        var traceId = Activity.Current?.Id ?? context.TraceIdentifier;
        var cancellationToken = context.RequestAborted;

        var token = context.User.FindFirstValue(CustomClaimType.AccessToken);

        if (string.IsNullOrWhiteSpace(token))
        {
            logger.LogWarning("Missing or invalid Authorization header. traceId={TraceId}.", traceId);

            await WriteProblemAsync(context);
            return;
        }

        var introspectionEndpoint = KeycloakEndpoints
            .Introspect(identityOptions.Realm)
            .TrimStart('/');

        using var httpClient = httpClientFactory.CreateClient(Components.KeyCloak);

        using var requestContent = new FormUrlEncodedContent([
            new("token", token),
            new("client_id", identityOptions.ClientId),
            new("client_secret", identityOptions.ClientSecret),
        ]);

        using var response = await httpClient.PostAsync(
            introspectionEndpoint,
            requestContent,
            cancellationToken
        );

        if (!response.IsSuccessStatusCode)
        {
            logger.LogWarning("Token introspection returned {StatusCode}, traceId={TraceId}.", response.StatusCode, traceId);

            await WriteProblemAsync(context);
            return;
        }

        await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        using var tokenResponse = await JsonDocument.ParseAsync(
            stream,
            cancellationToken: cancellationToken
        );

        var isActive =
            tokenResponse.RootElement.TryGetProperty("active", out var activeElement)
            && activeElement.GetBoolean();

        if (!isActive)
        {
            logger.LogWarning("Inactive token presented, traceId={TraceId}.", traceId);

            await WriteProblemAsync(context);
            return;
        }

        await next(context);
    }

    private static Task WriteProblemAsync(HttpContext context)
    {
        return context
                .WriteProblemAsync(
                    title: IdentityErrorCode.Unauthorized.ToDisplayString(),
                    statusCode: StatusCodes.Status401Unauthorized);
    }
}
