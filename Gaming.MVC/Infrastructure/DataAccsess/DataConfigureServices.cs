using Gaming.MVC.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gaming.MVC.Infrastructure.DataAccsess;

public static class DataConfigureServices
{
    public static IServiceCollection AddDataConfiguration(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
        {
            options.UseLazyLoadingProxies();
            options.UseNpgsql(configuration.GetConnectionString("DbConnection"));
        });

        return services;
    }
}