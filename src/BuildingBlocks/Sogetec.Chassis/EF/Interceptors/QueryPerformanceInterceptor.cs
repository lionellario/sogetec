using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Sogetec.Chassis.EF.Interceptors;

public sealed class QueryPerformanceInterceptor(ILogger<QueryPerformanceInterceptor> logger) : DbCommandInterceptor
{
    private const long QueryThreshold = 100;

    public override ValueTask<DbDataReader> ReaderExecutedAsync(
        DbCommand command,
        CommandExecutedEventData eventData,
        DbDataReader result,
        CancellationToken cancellationToken = default)
    {
        var duration = eventData.Duration.TotalMilliseconds;

        if (duration > QueryThreshold)
        {
            var commandText = command.CommandText;
            if (command.Parameters.Count > 0)
            {
                commandText += " | Parameters: " + string.Join(", ", command.Parameters);
            }

            logger.LogWarning(
                "Slow query detected: {CommandText} | Elapsed time: {ElapsedMilliseconds} ms",
                commandText,
                duration
            );
        }
        return base.ReaderExecutedAsync(command, eventData, result, cancellationToken);
    }

    public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
    {
        var duration = eventData.Duration.TotalMilliseconds;

        if (duration > QueryThreshold)
        {
            var commandText = command.CommandText;
            if (command.Parameters.Count > 0)
            {
                commandText += " | Parameters: " + string.Join(", ", command.Parameters);
            }

            logger.LogWarning(
                "Slow query detected: {CommandText} | Elapsed time: {ElapsedMilliseconds} ms",
                commandText,
                duration
            );
        }
        return base.ReaderExecuted(command, eventData, result);
    }
}
