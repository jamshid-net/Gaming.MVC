using Gaming.Application.Common.Interfaces;
using Gaming.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.DependencyInjection;

namespace Gaming.Infrastructure.DataAccsess;

public static class DataConfigureServices
{
    public static IServiceCollection AddDataConfiguration(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
        {
            options.UseLazyLoadingProxies();
            options.UseNpgsql(configuration.GetConnectionString("DbConnection"));
        });
        services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
          .AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
        return services;
    }
}