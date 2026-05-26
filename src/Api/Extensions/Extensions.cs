using Api.Modules.Data.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Serilog;
using Sogetec.Chassis.Caching;
using Sogetec.Chassis.CQRS;
using Sogetec.Chassis.EF;
using Sogetec.Chassis.EF.Interceptors;
using Sogetec.Chassis.Logging;
using Sogetec.Chassis.OpenTelemetry;
using Sogetec.Chassis.Repository;
using Sogetec.Chassis.Security.Authorization;
using Sogetec.Chassis.Security.Extensions;
using Sogetec.Constants.Aspire;
using Sogetec.ServiceDefaults.Cors;

namespace Api.Extensions;

public static class Extensions
{
    public static void AddApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.ConfigureHttpJsonOptions(o =>
        {
            o.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            o.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        builder.Services
            .AddSerilog((services, loggerConfig) => loggerConfig
                .ReadFrom.Configuration(builder.Configuration)
                .ReadFrom.Services(services))
            .AddApplicationAuthLogEnricher();

        builder.Services.AddOpenApi();

        // Exception handlers
        builder.Services
            .AddProblemDetails(options =>
            {
                options.CustomizeProblemDetails = ctx =>
                {
                    ctx.ProblemDetails.Extensions["traceId"] = ctx.HttpContext.TraceIdentifier;
                    ctx.ProblemDetails.Extensions["timestamp"] = DateTimeOffset.UtcNow;
                    ctx.ProblemDetails.Instance = $"{ctx.HttpContext.Request.Method} {ctx.HttpContext.Request.Path}";
                };
            })
            .AddNotFoundExceptionHandler()
            .AddInvalidOperationExceptionHandler()
            .AddValidationExceptionHandler()
            .AddGlobalExceptionHandler();

        // Security
        builder.AddDefaultCors();
        builder.AddDefaultAuthentication().WithKeycloakClaimsTransformation();
        builder.AddDefaultAuthorization();
        builder.Services.AddScoped<ISessionDataProvider, SessionDataProvider>();

        // API Rate Limiting and Caching
        builder.Services.AddVersioning();
        builder.AddRateLimiting();
        builder.AddCaching();

        // Persistence interceptors
        builder.Services.AddPreAuthenticationRepositories(typeof(IPreAuthenticationRepository));
        builder.Services.AddScoped<IInterceptor, EventDispatchInterceptor>();
        builder.Services.AddScoped<IInterceptor, QueryPerformanceInterceptor>();

        // Application Modules or Services
        builder.Services.AddValidatorsFromAssembly(typeof(Extensions).Assembly, includeInternalTypes: true);

        builder.AddPostgresDbContext<SogetecDbContext>(Components.DatabaseName.Sogetec);

        builder.Services.AddEndpoints(typeof(Extensions));

        // Pipelines
        builder.Services
            .AddMediator(options => options.ServiceLifetime = ServiceLifetime.Scoped)
            .AddMediatorDomainEventDispatcher()
            .ApplyLoggingBehavior()
            .ApplyActivityBehavior()
            .ApplyValidationBehavior();

        // OTEL: Metrics & Traces
        builder.Services
            .AddActivityScope()
            .AddCommandHandlerMetrics()
            .AddQueryHandlerMetrics();

        // Configure ClaimsPrincipal
        builder.Services.AddTransient(s =>
            s.GetRequiredService<IHttpContextAccessor>().HttpContext?.User
            ?? new ClaimsPrincipal()
        );

        // builder.Services
        //     .AddKeycloakTokenIntrospection()
        //     .AddKeycloakServices();
    }

    public static void AddApplicationMiddlewares(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference(o =>
            {
                o.Theme = ScalarTheme.BluePlanet;
            });
        }
        else
        {
            app.UseHsts();
        }

        app.UseExceptionHandler();

        app.UseHttpsRedirection();

        app.UseDefaultCors();

        app.UseFileServer();

        app.UseSerilogRequest();

        app.UseAuthentication();

        app.UseAuthorization();

        // app.UseKeycloakTokenIntrospection();

        app.UseApplicationAuthLogEnricher();

        app.UseRateLimiter();

        var apiVersionSet = app.NewApiVersionSet().HasApiVersion(new(1, 0)).ReportApiVersions().Build();

        app.MapEndpoints(apiVersionSet);

        app.MapDefaultEndpoints();

        app.MapFallbackToFile("index.html");
    }
}
