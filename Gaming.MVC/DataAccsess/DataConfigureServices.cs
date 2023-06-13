using Gaming.MVC.Services;
using Microsoft.EntityFrameworkCore;

namespace Gaming.MVC.DataAccsess;

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