namespace Sogetec.Chassis.OpenTelemetry;

public static class TelemetryTags
{
    public static class Commands
    {
        public const string Command = $"{ActivitySourceProvider.DefaultSourceName}.command";
        public const string CommandType = $"{Command}.type";
        private const string CommandsMeter = $"{ActivitySourceProvider.DefaultSourceName}.commands";
        private const string CommandHandling = $"{CommandsMeter}.handling";
        public const string ActiveCommandsNumber = $"{CommandHandling}.active.count";
        public const string TotalCommandsNumber = $"{CommandHandling}.total.count";
        public const string CommandHandlingDuration = $"{CommandHandling}.duration";
    }

    public static class Queries
    {
        public const string Query = $"{ActivitySourceProvider.DefaultSourceName}.query";
        public const string QueryType = $"{Query}.type";
        private const string QueriesMeter = $"{ActivitySourceProvider.DefaultSourceName}.queries";
        private const string QueryHandling = $"{QueriesMeter}.handling";
        public const string ActiveQueriesNumber = $"{QueryHandling}.active.count";
        public const string TotalQueriesNumber = $"{QueryHandling}.total.count";
        public const string QueryHandlingDuration = $"{QueryHandling}.duration";
    }

    public static class Tenants
    {
        public const string TenantTrace = $"{ActivitySourceProvider.DefaultSourceName}.tenant";
        public const string TenantMeter = $"{ActivitySourceProvider.DefaultSourceName}.tenant";
        public const string Domain = $"{TenantTrace}.domain";
        public const string Status = $"{TenantTrace}.status";
        public const string ActiveRequests = $"{TenantMeter}.active_requests.count";
        public const string TenantRequests = $"{TenantMeter}.requests.count";
        public const string RequestDuration = $"{TenantMeter}.duration";
    }

    public static class EventHandling
    {
        public const string Event = $"{ActivitySourceProvider.DefaultSourceName}.event";
    }

    internal static class Validator
    {
        public const string Validation = $"{ActivitySourceProvider.DefaultSourceName}.validator";
    }

    public static class Http
    {
        public const string Method = "http.request.method";
    }
}
