using System.Text.Json.Serialization;
using Sogetec.Chassis.Security.Keycloak.Models;

namespace Sogetec.Chassis.Security.Keycloak.Context;

[JsonSerializable(typeof(GroupRepresentation))]
[JsonSerializable(typeof(TokenRepresentation))]
[JsonSerializable(typeof(UserRepresentation))]
[JsonSerializable(typeof(UserStatusRepresentation))]
[JsonSerializable(typeof(List<UserRepresentation>))]
[JsonSerializable(typeof(List<RoleRepresentation>))]
public partial class KeycloakJsonContext : JsonSerializerContext
{
}
