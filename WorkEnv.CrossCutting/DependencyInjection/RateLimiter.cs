using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace WorkEnv.CrossCutting.DependencyInjection;

public static class RateLimiter
{
    public static IServiceCollection AddRateLimiter(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpcontext =>
                RateLimitPartition.GetTokenBucketLimiter(httpcontext.User.Identity?.Name ??
                                                         httpcontext.Request.Headers.Host.ToString(),
                    partition => new TokenBucketRateLimiterOptions
                    {
                        TokenLimit = 30,
                        ReplenishmentPeriod = TimeSpan.FromSeconds(5),
                        TokensPerPeriod = 26,
                        AutoReplenishment = true,
                        QueueLimit = 0,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                    }));
        });

        return services;
    }
}