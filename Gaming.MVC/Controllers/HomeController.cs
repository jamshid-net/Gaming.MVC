using Gaming.Domain.Entities;
using Gaming.MVC.Models;
using LazyCache;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Diagnostics;
using X.PagedList;

namespace Gaming.MVC.Controllers;

[Authorize]
public class HomeController : BaseController
{
    private readonly ILogger<HomeController> _logger;
   
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        
    }

    [EnableCors(PolicyName = "pdpmetan")]
    
    public async ValueTask<IActionResult> Index()
    {

        var entity = await _context.Products.ToListAsync();

        return View(entity);
    }

    [EnableRateLimiting("FixedLimiter")]
    public async Task<IActionResult> OurShop(int page =1)
    {

        int pageSize = 10;
        var products = new List<Product>();
        var categories = new List<Category>();  
        if(_appCache.TryGetValue("categoriesOurShop", out List<Category> categoriesCache))
        {
            categories = categoriesCache;
        }   
        else
        {
            categories = await _context.Categories.ToListAsync();
            _appCache.Add("categoriesOurShop", categories);
        }

        if(_appCache.TryGetValue("productsOurShop",out List<Product> productsCache))
        {
            products = productsCache;
        }
        else
        {
            products = await _context.Products.ToListAsync();
            _appCache.Add("productsOurShop", products);
        }
        
        var paginatedList = products.ToPagedList(page, pageSize);
        var ProductAndCategoryViewModels = new ProductAndCategoryViewModels()
        {
            Products = paginatedList,
            Categories = categories
        };


        return View(ProductAndCategoryViewModels);
    }

    [EnableRateLimiting("SlidingLimiter")]
    public async Task<IActionResult> ProductDetails(Guid? id)
    {
        if(id == null)
        {
            return RedirectToAction("OurShop");
        }

        var entity =await _context.Products.FindAsync(new object[] { id });
        return View(entity);
    }


    [EnableRateLimiting("SlidingLimiter")]
    [Authorize(Roles ="farfar")]
    public IActionResult Contact()
    {
        return View();
    }

    [EnableRateLimiting("SlidingLimiter")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
