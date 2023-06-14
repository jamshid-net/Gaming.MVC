using FluentValidation;
using Gaming.Application.Common.Interfaces;
using Gaming.Application.Common.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Gaming.Application;
public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        
        services.AddScoped<IHashStringService, HashStringService>();
       
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(option =>
        {
            option.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());


        });

        return services;
    }
}
