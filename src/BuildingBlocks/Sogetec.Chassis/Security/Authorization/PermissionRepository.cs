// using NpgsqlTypes;

// namespace Sogetec.Chassis.Security.Authorization;

// public interface IPermissionRepository : IPreAuthenticationRepository
// {
//     Task<HashSet<string>> GetUserPermissionsAsync(
//         IReadOnlyList<string>? roleNames = null,
//         CancellationToken cancellationToken = default);
// }

// public sealed class PermissionRepository(IConfiguration configuration) : IPermissionRepository
// {
//     public async Task<HashSet<string>> GetUserPermissionsAsync(
//         IReadOnlyList<string>? roleNames = null,
//         CancellationToken cancellationToken = default)
//     {
//         var connectionString = configuration.GetConnectionString(Components.DatabaseName.User)
//                                 ?? throw new InvalidOperationException("Missing connection string.");

//         const string sql = """
//             SELECT DISTINCT p.name
//             FROM role_permission rp
//             INNER JOIN permission p ON p.permission_id = rp.permission_id
//             INNER JOIN role r ON r.role_id = rp.role_id
//             WHERE
//                 @roleNames IS NULL
//                 OR r.name = ANY(@roleNames)
//             """;

//         await using var conn = new NpgsqlConnection(connectionString);
//         await conn.OpenAsync(cancellationToken);

//         await using var cmd = new NpgsqlCommand(sql, conn);

//         if (roleNames is { Count: > 0 })
//         {
//             cmd.Parameters.Add(
//                 new NpgsqlParameter<string[]>("roleNames", [.. roleNames])
//                 {
//                     NpgsqlDbType = NpgsqlDbType.Array | NpgsqlDbType.Text
//                 });
//         }
//         else
//         {
//             cmd.Parameters.AddWithValue("roleNames", DBNull.Value);
//         }

//         var permissions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

//         await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);

//         while (await reader.ReadAsync(cancellationToken))
//         {
//             permissions.Add(reader.GetString(0));
//         }

//         return permissions;
//     }
// }
