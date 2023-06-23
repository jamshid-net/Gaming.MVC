using Microsoft.AspNetCore.Mvc;

namespace Gaming.MVC.Controllers;
public class PermissionsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
