using Sudzekai.WebOs.Core;
using System.Threading.RateLimiting;

namespace Presentation.src.Extensions.ServiceCollection
{
    internal static class ServiceCollectionRateLimitingExtensions
    {
        public static void ConfigureRateLimiting(this IServiceCollection services)
        {
            services.AddRateLimiter(o =>
            {
                o.AddPolicy(RateLimitingPolicy.Default.ToString(), ctx =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: ctx.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 100,
                            Window = TimeSpan.FromMinutes(1),
                            QueueLimit = 0
                        }
                    )
                );

                o.AddPolicy(RateLimitingPolicy.Anonymous.ToString(), ctx =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: "anon:" + (ctx.Connection.RemoteIpAddress?.ToString() ?? "unknown"),
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 30,
                            Window = TimeSpan.FromMinutes(1),
                            QueueLimit = 0
                        }
                    )
                );

                o.AddPolicy(RateLimitingPolicy.Authenticated.ToString(), ctx =>
                    RateLimitPartition.GetSlidingWindowLimiter(
                        partitionKey: ctx.User.Identity?.IsAuthenticated == true
                            ? "user:" + ctx.User.Identity.Name
                            : "ip:" + (ctx.Connection.RemoteIpAddress?.ToString() ?? "unknown"),
                        factory: _ => new SlidingWindowRateLimiterOptions
                        {
                            PermitLimit = 300,
                            Window = TimeSpan.FromMinutes(1),
                            SegmentsPerWindow = 6,
                            QueueLimit = 0
                        }
                    )
                );

                o.AddPolicy(RateLimitingPolicy.Heavy.ToString(), ctx =>
                    RateLimitPartition.GetTokenBucketLimiter(
                        partitionKey: ctx.User.Identity?.IsAuthenticated == true
                            ? "user:" + ctx.User.Identity.Name
                            : "ip:" + (ctx.Connection.RemoteIpAddress?.ToString() ?? "unknown"),
                        factory: _ => new TokenBucketRateLimiterOptions
                        {
                            TokenLimit = 10,
                            TokensPerPeriod = 5,
                            ReplenishmentPeriod = TimeSpan.FromMinutes(1),
                            QueueLimit = 0,
                            AutoReplenishment = true
                        }
                    )
                );

                o.AddPolicy(RateLimitingPolicy.Strict.ToString(), ctx =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: "strict:" + (ctx.User.Identity?.IsAuthenticated == true
                            ? "user:" + ctx.User.Identity.Name
                            : "ip:" + (ctx.Connection.RemoteIpAddress?.ToString() ?? "unknown")),
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 5,
                            Window = TimeSpan.FromMinutes(1),
                            QueueLimit = 0
                        }
                    )
                );
            });
        }
    }
}