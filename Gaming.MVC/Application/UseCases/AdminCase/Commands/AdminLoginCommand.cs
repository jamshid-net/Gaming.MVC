using Gaming.MVC.Application.Common.Exceptions;
using Gaming.MVC.Application.Common.Interfaces;
using Gaming.MVC.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Gaming.MVC.Application.UseCases.AdminCase.Commands;

public class AdminLoginCommand:IRequest<ClaimsIdentity>
{
    public string AdminName { get; set; }
    public string Password { get; set; }
}


public class AdminLoginCommandHandler : IRequestHandler<AdminLoginCommand, ClaimsIdentity>
{
    private readonly IApplicationDbContext _context;
    private readonly IHashStringService _hashStringService;

    public AdminLoginCommandHandler(IApplicationDbContext context, IHashStringService hashStringService)
    {
        _context = context;
        _hashStringService = hashStringService;
    }

    public async Task<ClaimsIdentity> Handle(AdminLoginCommand request, CancellationToken cancellationToken)
    {
        string hashedPassword = await _hashStringService.GetHashStringAsync(request.Password);
        var entity = _context.Admins.SingleOrDefault(x => x.AdminName == request.AdminName && x.Password == hashedPassword);
        if (entity is null)
            throw new NotFoundException(nameof(Admin), request.AdminName);

        var claims = new List<Claim>() 
        { 
            new Claim(ClaimTypes.Name, request.AdminName),
            new Claim(ClaimTypes.Role, entity.Role)
        };
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme,
            ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);
        return claimsIdentity;
    }
}
