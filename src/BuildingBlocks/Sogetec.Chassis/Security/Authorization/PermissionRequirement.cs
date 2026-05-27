using Microsoft.AspNetCore.Authorization;

namespace Sogetec.Chassis.Security.Authorization;

public record PermissionRequirement(string Permission) : IAuthorizationRequirement;
