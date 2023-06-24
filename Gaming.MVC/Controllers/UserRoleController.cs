using Gaming.Application.Common.Exceptions;
using Gaming.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gaming.MVC.Controllers;
public class UserRoleController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;



    public UserRoleController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public IActionResult Index()
    {
        var users = _userManager.Users.ToList();

        return View(users);

    }

    public IActionResult EditUserRole(string UserId)
    {
        var foundUser = _userManager.Users.SingleOrDefault(x=> x.Id == UserId);
        if (foundUser is null)
            throw new NotFoundException(nameof(Domain.Identity.User),UserId);
        
       var foundRoles = _userManager.GetRolesAsync(foundUser);

        //to be continue
        return View(foundRoles);
    }

}