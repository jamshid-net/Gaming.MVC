using FluentValidation;
using Gaming.MVC.Application.Common.Interfaces;
using Gaming.MVC.Application.Common.Services;
using System.Reflection;

namespace Gaming.MVC;

public static class Startup
{
    public static IServiceCollection AddStartup(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUser,CurrentUserService>();
        services.AddHttpContextAccessor();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(option =>
        {
            option.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());


        });

        return services;
    }
}
