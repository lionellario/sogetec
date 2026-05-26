using System.Text.Json.Serialization;

namespace Sogetec.Chassis.Security.Keycloak.Models.Users;

public sealed record UserRepresentation
(
    [property: JsonPropertyName("id")] Guid? KcUserId,
    string Username,
    string FirstName,
    string LastName,
    string Email,
    bool EmailVerified,
    bool Enabled,
    bool Totp,
    UserAttributesRepresentation? Attributes,
    List<CredentialRepresentation>? Credentials,
    List<string>? Groups,
    List<string>? RequiredActions
);

public sealed record CredentialRepresentation
(
    string Type,
    string Value,
    bool Temporary
);

public sealed record UserAttributesRepresentation
(
    List<Guid> UserId
);
