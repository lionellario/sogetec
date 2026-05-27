namespace Sogetec.Chassis.Security.Keycloak.Services;

public interface IKeycloakTokenProvider
{
    /// <summary>
    /// Authenticate to keycloak and return an access token to be used to make REST API calls to keycloak.<br/>
    /// It authenticates to keycloak backend client using the "client_credentials" grant type, client_id and client_secret.<br/>
    /// Creates an active admin session and returns the access token.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>Access Token</returns>
    Task<string> GetAccessTokenAsync(CancellationToken cancellationToken = default);
}
