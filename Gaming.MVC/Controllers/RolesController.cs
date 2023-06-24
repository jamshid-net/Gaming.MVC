using Gaming.Application.Common.Exceptions;
using Gaming.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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

        var Claims = await _roleManager.GetClaimsAsync(foundRole);
        UpdateClaimModelView updateClaimModelView = new UpdateClaimModelView();
        updateClaimModelView.RoleId = foundRole.Id;
        updateClaimModelView.RoleName = foundRole.Name;
        foreach (var item in Claims)
        {
            updateClaimModelView
                .UpdateClaims.Add(
                new UpdateClaim()
                {
                    ClaimName = item.Value,
                    isActiveClaim = true
                }
            );
        }


        return View(updateClaimModelView);    
        
    }

    [HttpPost]
    public async Task<IActionResult> Edit([FromForm]UpdateClaimModelView updateClaimModel)
    {

        
        var foundRole = await _roleManager.FindByIdAsync(updateClaimModel.RoleId);
        if (foundRole is null)
            throw new NotFoundException(nameof(IdentityRole), updateClaimModel.RoleId);

        var Claims = await _roleManager.GetClaimsAsync(foundRole);

        if (updateClaimModel.UpdateClaims.Count == Claims.Count)
        {
            for (int i = 0; i < Claims.Count; i++)
            {
                if (Claims[i].Value == updateClaimModel.UpdateClaims[i].ClaimName)
                {
                    if (!updateClaimModel.UpdateClaims[i].isActiveClaim)
                    {
                       await _roleManager.RemoveClaimAsync(foundRole, Claims[i]);
                        Claims.Remove(Claims[i]);
                        updateClaimModel.UpdateClaims.Remove(updateClaimModel.UpdateClaims[i]);
                        --i;
                        
                    }
                }


            }
        }
        await _roleManager.UpdateAsync(foundRole);
        

        return View(updateClaimModel);   
    }


    public async Task<IActionResult> CreateClaim(string roleId, string claimName)
    {
        var foundRole = await _roleManager.FindByIdAsync(roleId);
        if (foundRole is null)
            throw new NotFoundException(nameof(IdentityRole), roleId);

       await  _roleManager.AddClaimAsync(foundRole, new Claim(ClaimTypes.Role,claimName));
       await _roleManager.UpdateAsync(foundRole);

        return RedirectToAction("Edit",new{roleId=foundRole.Id});

    }





}
