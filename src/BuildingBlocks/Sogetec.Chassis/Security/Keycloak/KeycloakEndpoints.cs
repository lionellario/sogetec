namespace Sogetec.Chassis.Security.Keycloak;

public static class KeycloakEndpoints
{
    public static string Token(string realm)
    {
        return $"/realms/{realm}/protocol/openid-connect/token";
    }

    public static string Authorize(string realm)
    {
        return $"/realms/{realm}/protocol/openid-connect/auth";
    }

    public static string Introspect(string realm)
    {
        return $"/realms/{realm}/protocol/openid-connect/token/introspect";
    }

    public static string Users(
        string realm,
        Guid? kcUserId = null,
        bool logout = false,
        bool resetPassword = false,
        Dictionary<string, string>? queryParams = null)
    {
        var url = $"/admin/realms/{realm}/users";
        var query = new List<string>();

        if (kcUserId != null)
        {
            url += $"/{kcUserId}";
        }

        if (logout)
        {
            url += "/logout";
        }

        if (resetPassword)
        {
            url += "/reset-password";
        }

        if (queryParams != null)
        {
            query.AddRange(queryParams.Select(kvp => $"{kvp.Key}={kvp.Value}"));
        }

        var queryString = string.Join("&", query);

        return string.IsNullOrWhiteSpace(queryString) ? url : $"{url}?{queryString}";
    }

    public static string Roles(string realm)
    {
        return $"/admin/realms/{realm}/roles";
    }

    public static string Groups(
        string realm,
        Guid? groupId = null,
        Dictionary<string, string>? queryParams = null)
    {
        var url = $"/admin/realms/{realm}/groups";
        var query = new List<string>();

        if (groupId != null)
        {
            url += $"/{groupId}";
        }

        if (queryParams != null)
        {
            query.AddRange(queryParams.Select(kvp => $"{kvp.Key}={kvp.Value}"));
        }

        var queryString = string.Join("&", query);

        return string.IsNullOrWhiteSpace(queryString) ? url : $"{url}?{queryString}";
    }

    public static string UserRealmRoles(string realm, Guid userId)
    {
        return $"/admin/realms/{realm}/users/{userId}/role-mappings/realm";
    }

    public static string UserGroup(
        string realm,
        Guid kcUserId,
        Guid GroupId)
    {
        return $"/admin/realms/{realm}/users/{kcUserId}/groups/{GroupId}";
    }
}
