using System.Text.Json.Serialization;
using StackExchange.Redis;
using ZiggyCreatures.Caching.Fusion.Backplane;
using ZiggyCreatures.Caching.Fusion.Backplane.StackExchangeRedis;
using ZiggyCreatures.Caching.Fusion.Serialization.SystemTextJson;

namespace Sogetec.Chassis.Caching;

public static class CacheExtensions
{
    public static readonly FusionCacheSystemTextJsonSerializer Serializer
    //! IT'S IMPORTANT TO NOTE THAT STJ doesn't currently include
    //! internal members when serializing/deserializing.
        = new(new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            // Be forward-compatible: new enum values from a newer deploy
            // won't crash an older instance that reads the cached entry.
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
        });

    extension(IHostApplicationBuilder builder)
    {
        public IHostApplicationBuilder AddCaching()
        {
            builder.Configure<CachingOptions>(CachingOptions.ConfigurationSection);

            var cachingOptions = builder
                                    .Configuration
                                    .GetSection(CachingOptions.ConfigurationSection)
                                    .Get<CachingOptions>()!;

            // L2 distributed cache via Aspire Redis integration
            builder.AddRedisDistributedCache(Components.Redis);

            // Register the multiplexer so the backplane can reuse it.
            // This avoids opening a second connection pool purely for the backplane.
            builder.AddRedisClient(Components.Redis);

            builder.Services
                .AddSingleton<IFusionCacheBackplane>(sp => new RedisBackplane(
                    new RedisBackplaneOptions
                    {
                        // Reuse the registered multiplexer — no second connection pool.
                        ConnectionMultiplexerFactory = () => Task.FromResult(sp.GetRequiredService<IConnectionMultiplexer>())
                    }))
                .AddFusionCache()
                .WithRegisteredDistributedCache()
                .WithDefaultEntryOptions(new FusionCacheEntryOptions
                {
                    // 🧠 TTL STRATEGY
                    Duration = cachingOptions.Expiration,
                    JitterMaxDuration = TimeSpan.FromSeconds(30), // Spread expirations across N instances.

                    // 🧱 CACHE STAMPEDE PROTECTION
                    // prevents thundering herd on cold starts.
                    // Soft timeout: if the factory exceeds 2 s, return stale data immediately
                    // and let the factory finish in the background.
                    // Hard timeout: after 10 s the factory is abandoned entirely.
                    LockTimeout = TimeSpan.FromSeconds(2),
                    FactorySoftTimeout = TimeSpan.FromSeconds(2),
                    FactoryHardTimeout = TimeSpan.FromSeconds(10),

                    // 🛟 FAIL-SAFE (serve stale L1/L2 data when Redis or the DB is unavailable)
                    IsFailSafeEnabled = true,
                    FailSafeMaxDuration = TimeSpan.FromHours(2),
                    FailSafeThrottleDuration = TimeSpan.FromSeconds(10),

                    // 🌍 DISTRIBUTED BEHAVIOR
                    AllowBackgroundDistributedCacheOperations = true,

                    // 🔁 BACKGROUND REFRESH
                    EagerRefreshThreshold = 0.8f, // refresh when 80% of TTL is reached

                    // ⚠️ TIMEOUTS
                    DistributedCacheSoftTimeout = TimeSpan.FromSeconds(3),
                    DistributedCacheHardTimeout = TimeSpan.FromSeconds(5)
                })
                .WithSerializer(Serializer)
                .WithRegisteredBackplane() // picks up the singleton registered above
                .WithOptions(options =>
                {
                    // 🔑 VERSIONED KEYS (critical for deployments)
                    options.CacheKeyPrefix = $"{nameof(Sogetec)}:";
                    options.DistributedCacheKeyModifierMode = CacheKeyModifierMode.Prefix;

                    // 🔥 CIRCUIT BREAKER (protect Redis outages)
                    // Trip the circuit breaker after sustained Redis failures.
                    // 2 min gives Redis time to recover without your app hammering it.
                    options.DistributedCacheCircuitBreakerDuration = TimeSpan.FromMinutes(2);

                    // 📊 OBSERVABILITY
                    options.IncludeTagsInLogs = true;
                    options.IncludeTagsInMetrics = true;
                });

            return builder;
        }
    }
}
