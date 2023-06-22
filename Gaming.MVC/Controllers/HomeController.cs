using Gaming.Domain.Entities;
using Gaming.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
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

    [EnableRateLimiting("SlidingLimiter")]
    public async Task<IActionResult> OurShop(int page =1)
    {

        int pageSize = 10;

       
        var products = await _context.Products.ToListAsync();
        var categories = await _context.Categories.ToListAsync();
        var paginatedList=products.ToPagedList(page, pageSize);
        var ProductAndCategoryViewModels = new ProductAndCategoryViewModels()
        {
            Products = paginatedList,
            Categories = categories
        };
        return View(ProductAndCategoryViewModels);
    }
    public async Task<IActionResult> ProductDetails(Guid? id)
    {
        if(id == null)
        {
            return RedirectToAction("OurShop");
        }

        var entity =await _context.Products.FindAsync(new object[] { id });
        return View(entity);
    }

    public IActionResult Contact()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
