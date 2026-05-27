namespace Sogetec.Chassis.Caching;

public static class CacheProfiles
{
    extension(FusionCacheEntryOptions options)
    {
        public FusionCacheEntryOptions SetDistributedTimeouts(TimeSpan soft, TimeSpan hard)
        {
            options.DistributedCacheSoftTimeout = soft;
            options.DistributedCacheHardTimeout = hard;
            return options;
        }

        public FusionCacheEntryOptions SetJitter(TimeSpan jitter)
        {
            options.JitterMaxDuration = jitter;
            return options;
        }

        public FusionCacheEntryOptions EnableEagerRefresh(float threshold = 0.8f)
        {
            options.EagerRefreshThreshold = threshold;
            return options;
        }

        /// <summary>
        /// ⚡ Ultra-High Throughput (hot path)
        /// <strong>Requirements</strong>:
        ///     - extremely frequent calls
        ///     - low latency
        ///     - tolerate short staleness
        /// </summary>
        /// <returns>The <see cref="FusionCacheEntryOptions"/> so that additional calls can be chained.</returns>
        public FusionCacheEntryOptions CacheForHotPath()
        {
            return options
                    .SetDuration(TimeSpan.FromSeconds(30))
                    .SetJitter(TimeSpan.FromSeconds(5))
                    .SetFailSafe(true, TimeSpan.FromMinutes(5), TimeSpan.FromSeconds(10))
                    .SetFactoryTimeouts(TimeSpan.FromMilliseconds(100), TimeSpan.FromMilliseconds(500))
                    .SetDistributedTimeouts(TimeSpan.FromMilliseconds(50), TimeSpan.FromMilliseconds(200))
                    .EnableEagerRefresh(0.7f);
        }

        /// <summary>
        /// 🧠 Expensive-to-fetch, rarely changing
        /// <strong>Requirements</strong>:
        ///     - heavy DB queries
        ///     - tolerate stale data
        ///     - protect backend at all costs
        /// </summary>
        /// <returns>The <see cref="FusionCacheEntryOptions"/> so that additional calls can be chained.</returns>
        public FusionCacheEntryOptions CacheForExpensiveStable()
        {
            return options
                    .SetDuration(TimeSpan.FromMinutes(30))
                    .SetJitter(TimeSpan.FromMinutes(2))
                    .SetFailSafe(true, TimeSpan.FromHours(2), TimeSpan.FromSeconds(10))
                    .SetFactoryTimeouts(TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(10))
                    .EnableEagerRefresh();
        }

        /// <summary>
        /// 🧠 Expensive-to-fetch, frequent updates <br/>
        /// <strong>Requirements</strong>: <br/>
        ///     - heavy DB queries <br/>
        ///     - frequently updated by user <br/>
        ///     - must reflect changes relatively fast <br/>
        /// 👉 Important: Pair this with manual invalidation on write <br/>
        /// </summary>
        /// <returns>The <see cref="FusionCacheEntryOptions"/> so that additional calls can be chained.</returns>
        public FusionCacheEntryOptions CacheForExpensiveFrequent()
        {
            return options
                    .SetDuration(TimeSpan.FromMinutes(3))
                    .SetJitter(TimeSpan.FromSeconds(30))
                    .SetFailSafe(true, TimeSpan.FromMinutes(15), TimeSpan.FromSeconds(10))
                    .SetFactoryTimeouts(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(5))
                    .EnableEagerRefresh(0.75f);
        }

        /// <summary>
        /// 🌍 Global and Rarely changing  <br/>
        /// <strong>Requirements</strong>: <br/>
        ///     - global data <br/>
        ///     - rarely changes <br/>
        ///     - heavily shared across tenants <br/>
        /// </summary>
        /// <returns>The <see cref="FusionCacheEntryOptions"/> so that additional calls can be chained.</returns>
        public FusionCacheEntryOptions CacheForGlobalStatic()
        {
            return options
                    .SetDuration(TimeSpan.FromHours(6))
                    .SetFailSafe(true, TimeSpan.FromHours(24), TimeSpan.FromSeconds(10))
                    .SetFactoryTimeouts(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(5))
                    .SetJitter(TimeSpan.FromMinutes(10));
        }

        /// <summary>
        /// 👤Moderately accessed data <br/>
        /// <strong>Requirements</strong>: <br/>
        ///     - moderate traffic <br/>
        ///     - occasional updates <br/>
        ///     - should feel responsive <br/>
        /// </summary>
        /// <returns>The <see cref="FusionCacheEntryOptions"/> so that additional calls can be chained.</returns>
        public FusionCacheEntryOptions CacheForModerateStable()
        {
            return options
                    .SetDuration(TimeSpan.FromMinutes(10))
                    .SetFailSafe(true, TimeSpan.FromMinutes(30), TimeSpan.FromSeconds(10))
                    .SetFactoryTimeouts(TimeSpan.FromMilliseconds(500), TimeSpan.FromSeconds(3))
                    .SetJitter(TimeSpan.FromSeconds(20))
                    .EnableEagerRefresh();
        }

        public FusionCacheEntryOptions SetFrom(FusionCacheEntryOptions source)
        {
            options.Duration = source.Duration;
            options.JitterMaxDuration = source.JitterMaxDuration;

            options.LockTimeout = source.LockTimeout;
            options.FactorySoftTimeout = source.FactorySoftTimeout;
            options.FactoryHardTimeout = source.FactoryHardTimeout;

            options.IsFailSafeEnabled = source.IsFailSafeEnabled;
            options.FailSafeMaxDuration = source.FailSafeMaxDuration;
            options.FailSafeThrottleDuration = source.FailSafeThrottleDuration;

            options.AllowBackgroundDistributedCacheOperations = source.AllowBackgroundDistributedCacheOperations;

            options.EagerRefreshThreshold = source.EagerRefreshThreshold;

            options.DistributedCacheSoftTimeout = source.DistributedCacheSoftTimeout;
            options.DistributedCacheHardTimeout = source.DistributedCacheHardTimeout;

            return options;
        }
    }
}
