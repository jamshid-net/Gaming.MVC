using Gaming.Application.Common.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gaming.MVC.Controllers;
public class RolesController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;

    

    public RolesController(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
       
       
    }

    public async Task<IActionResult> Index()
    {
        var roles =await  _roleManager.Roles.ToListAsync();
        
        return View(roles);

    }

    public async Task<IActionResult> Create(string roleName)
    {
        var role = new IdentityRole(roleName);
        await _roleManager.CreateAsync(role);
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Delete(string roleId)
    {
       var foundRole = await _roleManager.FindByIdAsync(roleId);
        if (foundRole is null) 
            throw new NotFoundException(nameof(IdentityRole), roleId);  
        
       await _roleManager.DeleteAsync(foundRole);
        return RedirectToAction("Index");
    }


    public async Task<IActionResult> Edit(string roleId)
    {
        var foundRole = await _roleManager.FindByIdAsync(roleId);
        if (foundRole is null)
            throw new NotFoundException(nameof(IdentityRole), roleId);

        

        return View (foundRole);    
    }

    public async Task<IActionResult> CreateClaim(string roleId, string claimName)
    {
        var foundRole = await _roleManager.FindByIdAsync(roleId);
        if (foundRole is null)
            throw new NotFoundException(nameof(IdentityRole), roleId);

       await  _roleManager.AddClaimAsync(foundRole, new System.Security.Claims.Claim("Permission",claimName));
       await _roleManager.UpdateAsync(foundRole);

        return View("Edit", foundRole);

    }





}
