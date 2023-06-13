using Microsoft.AspNetCore.Authentication.Cookies;

namespace Gaming.MVC.Application.Common.CookieAuthentication;

public static class CookieAuthenticationService
{
    public static IServiceCollection AddCookieAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                options.Cookie.MaxAge = options.ExpireTimeSpan;
                options.SlidingExpiration = true;
            });

        return services;
    }
}
