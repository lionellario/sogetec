using System.Text.Json.Serialization;

namespace Sogetec.Chassis.Security.Keycloak.Models.Groups;

public sealed record GroupRepresentation(
    [property: JsonPropertyName("id")] Guid? KcGroupId,
    string Name,
    string Description,
    string Path,
    GroupAttributesRepresentation? Attributes = null
);

public sealed record GroupAttributesRepresentation(
    List<long> TenantId,
    List<string> TenantDomain
);
