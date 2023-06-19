using LazyCache;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Gaming.MVC.Attributes;

public class RemoveLazyCacheAttribute : ActionFilterAttribute
{
    private static IAppCache? _cache;
    private static IConfiguration? _configuration;
    private static string? key;
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        _configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        _cache = context.HttpContext.RequestServices.GetRequiredService<IAppCache>();



        if (context.HttpContext.Request.Path == "/Products/Create")
            key = "Products";
      

        _cache.Remove(key);
        await next();
    }
}
