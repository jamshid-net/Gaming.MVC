using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Gaming.MVC.Models;

public class UpdateClaimModelView
{
    public string RoleId { get; set; }  
    public string RoleName { get; set; }    
    public List<UpdateClaim> UpdateClaims { get; set; } = new List<UpdateClaim>();  
}

public class UpdateClaim
{
    public string ClaimName { get; set; }
    public bool isActiveClaim { get; set; }

}
