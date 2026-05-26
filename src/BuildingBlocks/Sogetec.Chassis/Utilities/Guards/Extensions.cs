namespace Sogetec.Chassis.Utilities.Guards;

public static class GuardExtensions
{
    extension(Guard guard)
    {
        public Guid NotAuthenticated(string? userId)
        {
            if (!Guid.TryParse(userId, out var uid))
                throw new UnauthorizedAccessException("User is not authenticated.");

            return uid;
        }

        public List<long> InvalidTenant(IEnumerable<string>? tenantIds)
        {
            var ids = tenantIds?
                        .Select(x => long.TryParse(x, out var id) ? id : -1)
                        .Where(x => x > 0).ToList() ?? [];

            if (tenantIds is null || ids.Count != tenantIds.Count())
            {
                throw new UnauthorizedAccessException("Invalid Tenant, probably due to user is not authenticated.");
            }

            return ids;
        }

        public string InvalidSession(string? sessionId)
        {
            return string.IsNullOrWhiteSpace(sessionId)
                ? throw new UnauthorizedAccessException("User not in session.")
                : sessionId;
        }
    }
}
