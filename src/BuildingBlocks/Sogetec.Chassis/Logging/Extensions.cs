using Serilog;

namespace Sogetec.Chassis.Logging;

public static class Extensions
{
    extension(IServiceCollection services)
    {
        /// <summary>
        ///     Registers the application log enricher middleware in the dependency injection container.
        /// </summary>
        /// <returns>
        ///     The updated <see cref="IServiceCollection" /> instance.
        /// </returns>
        public IServiceCollection AddApplicationAuthLogEnricher()
        {
            return services.AddScoped<ApplicationEnricherMiddleware>();
        }
    }

    extension(IApplicationBuilder app)
    {
        /// <summary>
        /// Adds middleware for streamlined request logging. Instead of writing HTTP request information
        /// like method, path, timing, status code and exception details
        /// in several events, this middleware collects information during the request (including from
        /// <see cref="IDiagnosticContext"/>), and writes a single event at request completion. Add this
        /// in <c>Startup.cs</c> before any handlers whose activities should be logged.
        /// </summary>
        /// <returns>
        ///     The same <see cref="IApplicationBuilder" /> instance so additional middleware can be chained.
        /// </returns>
        public IApplicationBuilder UseSerilogRequest()
        {
            return app.UseSerilogRequestLogging(options =>
            {
                options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                {
                    diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
                    diagnosticContext.Set("UserAgent", httpContext.Request.Headers.UserAgent.ToString());
                    diagnosticContext.Set("ClientIp", httpContext.Connection.RemoteIpAddress?.ToString());
                };

                // Exclude health check endpoints from request logs
                options.GetLevel = (httpContext, elapsed, ex) =>
                {
                    if (Http.Endpoints.IsExcludedPath(httpContext.Request.Path))
                    {
                        return Serilog.Events.LogEventLevel.Verbose;
                    }

                    return elapsed > 500
                        ? Serilog.Events.LogEventLevel.Warning
                        : Serilog.Events.LogEventLevel.Information;
                };
            });
        }

        /// <summary>
        ///     Adds the application log enricher middleware to the application request pipeline.
        /// </summary>
        /// <returns>
        ///     The same <see cref="IApplicationBuilder" /> instance so additional middleware can be chained.
        /// </returns>
        public IApplicationBuilder UseApplicationAuthLogEnricher()
        {
            return app.UseMiddleware<ApplicationEnricherMiddleware>();
        }
    }
}
