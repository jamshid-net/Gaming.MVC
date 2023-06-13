using FluentValidation;
using Gaming.MVC.DataAccsess;
using Gaming.MVC.Services;
using System.Reflection;

namespace Gaming.MVC;

public static class Startup
{
    public static IServiceCollection AddStartup(this IServiceCollection services)
    {

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(option =>
        {
            option.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());


        });

        return services;
    }
}
