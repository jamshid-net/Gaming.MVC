using Gaming.Application;
using Gaming.Application.Common.CookieAuthentication;
using Gaming.Infrastructure.DataAccsess;
using Gaming.MVC.RateLimiterService;
using Gaming.MVC.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Gaming.MVC;

public class Program
{

    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);



        CorsPolicy pdp = new CorsPolicy()
        {
            Headers = { "pdp", "unicorn", "bootcamp" },
            Origins = { "https://online.pdp.uz" },
            Methods = {"GET","DELETE"}


        };

         
        

        builder.Services.AddCors(setup =>
        {
            setup.AddPolicy("pdp", pdp); 

           
        });
        

        //builder.Services.AddCors(builderPolicy =>
        //{
        //    builderPolicy.AddPolicy("metan", builder =>
        //    {
        //        builder.WithOrigins("https://metanit.com")
        //        .WithHeaders("metan", "salom");
        //    });

        //});
        


        builder.Services.AddRazorPages();
        builder.Services.AddControllersWithViews().AddNewtonsoftJson();
        builder.Services.AddDataConfiguration(builder.Configuration);
        builder.Services.AddStartup();
        builder.Services.AddApplication();
        builder.Services.AddCookieAuthentication();

        

        builder.Services.AddAuthentication().AddGoogle(options =>
        {
            options.ClientId = builder?.Configuration["web:client_id"];

            options.ClientSecret = builder?.Configuration["web:client_secret"];
        });

        builder.Services.AddAuthentication().AddFacebook(options =>
        {
            options.ClientId = builder?.Configuration["facebook:client_id"];

            options.ClientSecret = builder?.Configuration["facebook:client_secret"];
        });

        builder.Services.AddAuthentication().AddGitHub(options =>
        {
            options.ClientId = builder?.Configuration["github:client_id"];

            options.ClientSecret = builder?.Configuration["github:client_secret"];

        });

        //redis cache add service 

        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = builder.Configuration.GetConnectionString("RedisCache");

        });


        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("RoleBasedClaim", policy => policy.RequireClaim("Permission", "true"));
        });
        builder.Services.AddLazyCache();
        builder.Services.AddRatelimiterParams();
        builder.Services.AddSingleton<TelegramBotService>();


        var app = builder.Build();
        // app.UseExceptionHandler();


        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        //app.UseCors("pdpPolicy");
        //app.UseCors("metanit");


        app.UseCors("pdp");


        //app.UseCors(builder => builder
        //.WithOrigins("https://online.pdp.uz", "https://metanit.com")
        //.WithHeaders("mycustom")

        //);




        app.UseRateLimiter();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.MapRazorPages();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        using (var scope = app.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = new[] { "admin", "user", "employee" };
            foreach (var item in roles)
            {
                if (!(await roleManager.RoleExistsAsync(item)))
                    await roleManager.CreateAsync(new IdentityRole(item));
            }
        }

        app.Run();
    }
}
