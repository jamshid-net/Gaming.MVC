using Microsoft.AspNetCore.RateLimiting;

namespace Gaming.MVC.RateLimiterService;

public static class WebSiteRateLimeter
{
    public static IServiceCollection AddRatelimiterParams(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.AddFixedWindowLimiter("FixedLimiter", opt =>
            {
                opt.Window = TimeSpan.FromSeconds(10);
                opt.QueueLimit = 5;
                opt.PermitLimit = 15;
                opt.AutoReplenishment = true;
                opt.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;


            });
            options.AddSlidingWindowLimiter("SlidingLimiter", opt =>
            {
                opt.Window = TimeSpan.FromSeconds(10);
                opt.QueueLimit = 5;
                opt.PermitLimit = 15;
                opt.AutoReplenishment = true;
                opt.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
                opt.SegmentsPerWindow = 5;

            });

            options.AddConcurrencyLimiter("ConcurrencyLimiter", opt =>
            {
                opt.QueueLimit = 5;
                opt.PermitLimit = 15;
                opt.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
                
            });

            options.AddTokenBucketLimiter("TokenBucketLimiter", opt =>
            {
                opt.QueueLimit = 5;
                opt.TokenLimit = 20;
                opt.AutoReplenishment= true;
                opt.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
                opt.TokensPerPeriod = 5;
                opt.ReplenishmentPeriod = TimeSpan.FromSeconds(10);
                

            });
            

            
        });

        return services;

    }
}
