namespace Sogetec.Chassis.Security.Keycloak.Services;

public interface IKeycloakAdminClient
{
    /// <summary>
    /// Fetches and return the list of users configured in keycloak
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>List of users configured in keycloak.</returns>
    Task<List<UserRepresentation>> GetActiveUsersAsync(
        Dictionary<string, string>? queryParams = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches and return a user based on the user's keycloak ID.
    /// </summary>
    /// <param name="kcUserId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Single user configured in keycloak.</returns>
    Task<UserRepresentation?> GetUserByIdAsync(
        Guid kcUserId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Takes a user representation object and create a user in Keycloak.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The newly created keycloak user ID.</returns>
    Task<Guid> CreateUserAsync(
        UserRepresentation request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Disable a User in Keycloak using their user ID.
    /// </summary>
    /// <param name="kcUserId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DisableUserAsync(
        Guid kcUserId,
        UserStatusRepresentation request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Enable a User in Keycloak using their user ID.
    /// </summary>
    /// <param name="kcUserId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task EnableUserAsync(
        Guid kcUserId,
        UserStatusRepresentation request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete all sessions for a user using their keycloak's ID.
    /// </summary>
    /// <param name="kcUserId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteUserSessionsAsync(
        Guid kcUserId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Update a user's password in keycloak using the keycloak's user ID and the <see cref="CredentialRepresentation"/>.
    /// </summary>
    /// <param name="kcUserId"></param>
    /// <param name="credential"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task SetPasswordAsync(
        Guid kcUserId,
        CredentialRepresentation credential,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Fetches and returns a list of groups configured in keycloak.
    /// </summary>
    /// <param name="queryParams"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<GroupRepresentation>> GetGroupsAsync(
        Dictionary<string, string>? queryParams = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Add a new Keycloak group to the realm. Returns the ID of the newly created group.
    /// </summary>
    /// <param name="request" cref="GroupRepresentation">Object representing the group to be created</param>
    /// <param name="cancellationToken" cref="CancellationToken"></param>
    /// <returns>The ID of the newly created group in Keycloak.</returns>
    /// <exception cref="InvalidOperationException"></exception>
    Task<Guid> CreateGroupAsync(
        GroupRepresentation request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Add a user to a group in Keycloak.<br/>
    /// This method takes the user's Keycloak ID and the group's keycloak ID.<br/>
    /// It will add the user to the group in keycloak.<br/>
    /// </summary>
    /// <param name="kcUserId"></param>
    /// <param name="kcGroupId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task AddUserToGroupAsync(
        Guid kcUserId,
        Guid kcGroupId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove a user from a group in Keycloak.<br/>
    /// This method takes the user's Keycloak ID and the group's keycloak ID.<br/>
    /// It will remove the user from the group in keycloak.<br/>
    /// </summary>
    /// <param name="kcUserId"></param>
    /// <param name="kcGroupId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task RemoveUserFromGroupAsync(
        Guid kcUserId,
        Guid kcGroupId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get the list of Realm Roles configured in Keycloak.
    /// </summary>
    /// <param name="cancellationToken" cref="CancellationToken"></param>
    /// <returns>Returns a list of realm roles from Keycloak</returns>
    Task<List<RoleRepresentation>> GetRealmRolesAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Assign one or more realm roles to a user in Keycloak.<br/>
    /// This method takes the user's Keycloak ID and a list of role representations to be assigned.<br/>
    /// It will add the specified roles to the user's existing role mappings in Keycloak.<br/>
    /// </summary>
    /// <param name="kcUserId"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task AssignRealmRoleAsync(
        Guid kcUserId,
        List<RoleRepresentation> request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove one or more realm roles from a user in Keycloak.<br/>
    /// This method takes the user's Keycloak ID and a list of role representations to be removed.<br/>
    /// It will remove the specified roles from the user's existing role mappings in Keycloak.<br/>
    /// </summary>
    /// <param name="kcUserId"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task RemoveRealmRoleAsync(
        Guid kcUserId,
        List<RoleRepresentation> request,
        CancellationToken cancellationToken = default);
}
