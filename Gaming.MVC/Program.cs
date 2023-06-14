using Gaming.Application;
using Gaming.Application.Common.CookieAuthentication;
using Gaming.Infrastructure.DataAccsess;

namespace Gaming.MVC;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
       


        builder.Services.AddControllersWithViews();
        builder.Services.AddDataConfiguration(builder.Configuration);
        builder.Services.AddStartup();
        builder.Services.AddApplication();
        builder.Services.AddCookieAuthentication();
        builder.Services.AddAuthorization();
        var app = builder.Build();

       

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
