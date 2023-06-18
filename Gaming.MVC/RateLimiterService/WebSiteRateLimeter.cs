namespace Gaming.MVC.RateLimiterService;

public static class WebSiteRateLimeter
{
    public static IServiceCollection AddRatelimiterParams(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            
            
        });

        return services;

    }
}
