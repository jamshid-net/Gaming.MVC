using Gaming.MVC.Application.Common.Interfaces;
using Gaming.MVC.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Gaming.MVC.Application.UseCases.UserCase.Commands;

public class UserRegisterCommand : IRequest<ClaimsIdentity>
{
    public string UserName { get; init; }
    public string FullName { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public string PasswordConfirm { get; init; }

}
public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, ClaimsIdentity>
{
    private readonly IApplicationDbContext _context;
    private readonly IHashStringService _hashStringService;
   
  

    public UserRegisterCommandHandler(IApplicationDbContext context, IHashStringService hashStringService)
    {
        _context = context;
        _hashStringService = hashStringService;
        
    }

    public async Task<ClaimsIdentity> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
       
        string hashedPassword = await _hashStringService.GetHashStringAsync(request.Password);

        User user = new User()
        {
            FullName = request.FullName,
            Email = request.Email,
            Password = hashedPassword,
            UserName = request.UserName
            

        };
        _context.Users.Add(user);
        if (await _context.SaveChangesAsync(cancellationToken) > 0)
        {
            var claims = new List<Claim>() { new Claim(ClaimTypes.Name, request.UserName) };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme,
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;

        }
        return null;
    }
}