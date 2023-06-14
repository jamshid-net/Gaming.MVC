using Gaming.Application.Common.Interfaces;
using Gaming.Application.Common.Services;

namespace Gaming.MVC;

public static class Startup
{
    public static IServiceCollection AddStartup(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUser,CurrentUserService>();
        
        services.AddHttpContextAccessor();
      
        return services;
    }
}
