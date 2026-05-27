namespace Sogetec.Chassis.Security.Keycloak.Services;

public sealed class KeycloakAdminClient(
    IHttpClientFactory httpClientFactory,
    IKeycloakTokenProvider tokenProvider,
    IdentityOptions identityOptions
) : IKeycloakAdminClient
{
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        TypeInfoResolver = KeycloakJsonContext.Default,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public async Task<List<UserRepresentation>> GetActiveUsersAsync(
        Dictionary<string, string>? queryParams = null,
        CancellationToken cancellationToken = default)
    {
        var endpoint = KeycloakEndpoints
            .Users(realm: identityOptions.Realm, queryParams: queryParams)
            .TrimStart('/');

        var token = await tokenProvider.GetAccessTokenAsync(cancellationToken);

        using var httpClient = httpClientFactory.CreateClient(Components.KeyCloak);

        var users = await httpClient.SendAsync<List<UserRepresentation>>(
            method: HttpMethod.Get,
            url: endpoint,
            bearerToken: token,
            jsonOptions: _jsonOptions,
            cancellationToken: cancellationToken);

        return users?.Where(u => u.Enabled).ToList() ?? [];
    }

    public async Task<UserRepresentation?> GetUserByIdAsync(
        Guid kcUserId,
        CancellationToken cancellationToken = default)
    {
        var endpoint = KeycloakEndpoints
            .Users(realm: identityOptions.Realm, kcUserId: kcUserId)
            .TrimStart('/');

        var token = await tokenProvider.GetAccessTokenAsync(cancellationToken);

        using var httpClient = httpClientFactory.CreateClient(Components.KeyCloak);

        var user = await httpClient.SendAsync<UserRepresentation>(
            method: HttpMethod.Get,
            url: endpoint,
            bearerToken: token,
            jsonOptions: _jsonOptions,
            cancellationToken: cancellationToken);

        return user;
    }

    public async Task<Guid> CreateUserAsync(
        UserRepresentation request,
        CancellationToken cancellationToken = default)
    {
        var endpoint = KeycloakEndpoints
            .Users(identityOptions.Realm)
            .TrimStart('/');

        var token = await tokenProvider.GetAccessTokenAsync(cancellationToken);
        var kcUserId = Guid.Empty;

        using var httpClient = httpClientFactory.CreateClient(Components.KeyCloak);

        await httpClient.SendAsync(
            method: HttpMethod.Post,
            url: endpoint,
            bearerToken: token,
            body: request,
            jsonOptions: _jsonOptions,
            onResponse: response =>
            {
                if (!Guid.TryParse(response.Headers.Location?.Segments.Last(), out kcUserId))
                {
                    throw new InvalidOperationException("Failed to parse user ID from response.");
                }
            },
            cancellationToken: cancellationToken);

        return kcUserId;
    }

    public async Task UpdateUserAsync(
        Guid userId,
        UserRepresentation request,
        CancellationToken cancellationToken = default)
    {
        var endpoint = KeycloakEndpoints
            .Users(identityOptions.Realm, userId)
            .TrimStart('/');

        var token = await tokenProvider.GetAccessTokenAsync(cancellationToken);

        using var httpClient = httpClientFactory.CreateClient(Components.KeyCloak);

        await httpClient.SendAsync(
            method: HttpMethod.Put,
            url: endpoint,
            bearerToken: token,
            body: request,
            jsonOptions: _jsonOptions,
            cancellationToken: cancellationToken);
    }

    public async Task DisableUserAsync(
        Guid kcUserId,
        UserStatusRepresentation request,
        CancellationToken cancellationToken = default)
    {
        var endpoint = KeycloakEndpoints
            .Users(identityOptions.Realm, kcUserId)
            .TrimStart('/');

        var token = await tokenProvider.GetAccessTokenAsync(cancellationToken);

        using var httpClient = httpClientFactory.CreateClient(Components.KeyCloak);

        await httpClient.SendAsync(
            method: HttpMethod.Put,
            url: endpoint,
            bearerToken: token,
            body: request,
            jsonOptions: _jsonOptions,
            cancellationToken: cancellationToken);
    }

    public async Task EnableUserAsync(
        Guid kcUserId,
        UserStatusRepresentation request,
        CancellationToken cancellationToken = default)
    {
        var endpoint = KeycloakEndpoints
            .Users(identityOptions.Realm, kcUserId)
            .TrimStart('/');

        var token = await tokenProvider.GetAccessTokenAsync(cancellationToken);

        using var httpClient = httpClientFactory.CreateClient(Components.KeyCloak);

        await httpClient.SendAsync(
            method: HttpMethod.Put,
            url: endpoint,
            bearerToken: token,
            body: request,
            jsonOptions: _jsonOptions,
            cancellationToken: cancellationToken);
    }

    public async Task DeleteUserSessionsAsync(
        Guid kcUserId,
        CancellationToken cancellationToken = default)
    {
        var endpoint = KeycloakEndpoints
            .Users(realm: identityOptions.Realm, kcUserId: kcUserId, logout: true)
            .TrimStart('/');

        var token = await tokenProvider.GetAccessTokenAsync(cancellationToken);

        using var httpClient = httpClientFactory.CreateClient(Components.KeyCloak);

        await httpClient.SendAsync(
            method: HttpMethod.Post,
            url: endpoint,
            bearerToken: token,
            jsonOptions: _jsonOptions,
            cancellationToken: cancellationToken);
    }

    public async Task SetPasswordAsync(
        Guid kcUserId,
        CredentialRepresentation credential,
        CancellationToken cancellationToken = default)
    {
        var endpoint = KeycloakEndpoints
            .Users(realm: identityOptions.Realm, kcUserId: kcUserId, resetPassword: true)
            .TrimStart('/');

        var token = await tokenProvider.GetAccessTokenAsync(cancellationToken);

        using var httpClient = httpClientFactory.CreateClient(Components.KeyCloak);

        await httpClient.SendAsync(
            method: HttpMethod.Put,
            url: endpoint,
            bearerToken: token,
            body: credential,
            jsonOptions: _jsonOptions,
            cancellationToken: cancellationToken);
    }

    public async Task<List<GroupRepresentation>> GetGroupsAsync(
        Dictionary<string, string>? queryParams = null,
        CancellationToken cancellationToken = default)
    {
        var endpoint = KeycloakEndpoints
            .Groups(realm: identityOptions.Realm, queryParams: queryParams)
            .TrimStart('/');

        var token = await tokenProvider.GetAccessTokenAsync(cancellationToken);

        using var httpClient = httpClientFactory.CreateClient(Components.KeyCloak);

        var groups = await httpClient.SendAsync<List<GroupRepresentation>>(
            method: HttpMethod.Get,
            url: endpoint,
            bearerToken: token,
            jsonOptions: _jsonOptions,
            cancellationToken: cancellationToken);

        return groups ?? [];
    }

    public async Task<Guid> CreateGroupAsync(
        GroupRepresentation request,
        CancellationToken cancellationToken = default)
    {
        var endpoint = KeycloakEndpoints
            .Groups(identityOptions.Realm)
            .TrimStart('/');

        var token = await tokenProvider.GetAccessTokenAsync(cancellationToken);
        var kcGroupId = Guid.Empty;

        using var httpClient = httpClientFactory.CreateClient(Components.KeyCloak);

        await httpClient.SendAsync(
            method: HttpMethod.Post,
            url: endpoint,
            bearerToken: token,
            body: request,
            jsonOptions: _jsonOptions,
            onResponse: response =>
            {
                if (!Guid.TryParse(response.Headers.Location?.Segments.Last(), out kcGroupId))
                {
                    throw new InvalidOperationException("Failed to parse group ID from response.");
                }
            },
            cancellationToken: cancellationToken);

        return kcGroupId;
    }

    public async Task AddUserToGroupAsync(
        Guid kcUserId,
        Guid kcGroupId,
        CancellationToken cancellationToken = default)
    {
        var endpoint = KeycloakEndpoints
            .UserGroup(identityOptions.Realm, kcUserId, kcGroupId)
            .TrimStart('/');

        var token = await tokenProvider.GetAccessTokenAsync(cancellationToken);

        using var httpClient = httpClientFactory.CreateClient(Components.KeyCloak);

        await httpClient.SendAsync(
            method: HttpMethod.Put,
            url: endpoint,
            bearerToken: token,
            jsonOptions: _jsonOptions,
            cancellationToken: cancellationToken);
    }

    public async Task RemoveUserFromGroupAsync(
        Guid kcUserId,
        Guid kcGroupId,
        CancellationToken cancellationToken = default)
    {
        var endpoint = KeycloakEndpoints
            .UserGroup(identityOptions.Realm, kcUserId, kcGroupId)
            .TrimStart('/');

        var token = await tokenProvider.GetAccessTokenAsync(cancellationToken);

        using var httpClient = httpClientFactory.CreateClient(Components.KeyCloak);

        await httpClient.SendAsync(
            method: HttpMethod.Delete,
            url: endpoint,
            bearerToken: token,
            jsonOptions: _jsonOptions,
            cancellationToken: cancellationToken);
    }

    public async Task<List<RoleRepresentation>> GetRealmRolesAsync(
        CancellationToken cancellationToken = default)
    {
        var endpoint = KeycloakEndpoints
            .Roles(identityOptions.Realm)
            .TrimStart('/');

        var token = await tokenProvider.GetAccessTokenAsync(cancellationToken);

        using var httpClient = httpClientFactory.CreateClient(Components.KeyCloak);

        var roles = await httpClient.SendAsync<List<RoleRepresentation>>(
            method: HttpMethod.Get,
            url: endpoint,
            bearerToken: token,
            jsonOptions: _jsonOptions,
            cancellationToken: cancellationToken);

        return roles ?? [];
    }

    public async Task AssignRealmRoleAsync(
        Guid kcUserId,
        List<RoleRepresentation> request,
        CancellationToken cancellationToken = default)
    {
        var endpoint = KeycloakEndpoints
            .UserRealmRoles(identityOptions.Realm, kcUserId)
            .TrimStart('/');

        var token = await tokenProvider.GetAccessTokenAsync(cancellationToken);

        using var httpClient = httpClientFactory.CreateClient(Components.KeyCloak);

        await httpClient.SendAsync(
            method: HttpMethod.Post,
            url: endpoint,
            bearerToken: token,
            body: request,
            jsonOptions: _jsonOptions,
            cancellationToken: cancellationToken);
    }

    public async Task RemoveRealmRoleAsync(
        Guid kcUserId,
        List<RoleRepresentation> request,
        CancellationToken cancellationToken = default)
    {
        var endpoint = KeycloakEndpoints
            .UserRealmRoles(identityOptions.Realm, kcUserId)
            .TrimStart('/');

        var token = await tokenProvider.GetAccessTokenAsync(cancellationToken);

        using var httpClient = httpClientFactory.CreateClient(Components.KeyCloak);

        await httpClient.SendAsync(
            method: HttpMethod.Delete,
            url: endpoint,
            bearerToken: token,
            body: request,
            jsonOptions: _jsonOptions,
            cancellationToken: cancellationToken);
    }
}
