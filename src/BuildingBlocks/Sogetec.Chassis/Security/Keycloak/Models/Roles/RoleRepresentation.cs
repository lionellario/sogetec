using System.Text.Json.Serialization;

namespace Sogetec.Chassis.Security.Keycloak.Models.Roles;

public sealed record RoleRepresentation(
    [property: JsonPropertyName("id")] Guid KcRoleId,
    string Name
);
