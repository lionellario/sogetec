using System.Text.Json.Serialization;

namespace Sogetec.Chassis.Security.Keycloak.Models;

public sealed class TokenRepresentation
{
    [JsonPropertyName("access_token")]
    public required string AccessToken { get; init; }

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; init; }

    [JsonPropertyName("refresh_expires_in")]
    public int RefreshExpiresIn { get; init; }

    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; init; }

    [JsonPropertyName("token_type")]
    public required string TokenType { get; init; }

    [JsonPropertyName("session_state")]
    public string? SessionState { get; init; }

    [JsonPropertyName("scope")]
    public string? Scope { get; init; }
}
