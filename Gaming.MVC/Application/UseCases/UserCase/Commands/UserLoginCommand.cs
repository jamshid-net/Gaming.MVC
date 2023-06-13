using Gaming.MVC.Application.Common.Exceptions;
using Gaming.MVC.Application.Common.Interfaces;
using Gaming.MVC.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Gaming.MVC.Application.UseCases.UserCase.Commands;

public class UserLoginCommand:IRequest<ClaimsIdentity>
{
    public string UserName { get; init; }
    public string Password { get; init; }
}
public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, ClaimsIdentity>
{
    private readonly IApplicationDbContext _context;
    private readonly IHashStringService _hashStringService;

    public UserLoginCommandHandler(IApplicationDbContext context, IHashStringService hashStringService)
    {
        _context = context;
        _hashStringService = hashStringService;
    }

    public async Task<ClaimsIdentity> Handle(UserLoginCommand request, CancellationToken cancellationToken)
    {
        string hashedPassword = await _hashStringService.GetHashStringAsync(request.Password);
        var entity = _context.Users.SingleOrDefault(x => x.UserName == request.UserName && x.Password == hashedPassword);
        if (entity is null)
            throw new NotFoundException(nameof(User), request.UserName);

        var claims = new List<Claim>() { new Claim(ClaimTypes.Name, request.UserName) };
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme,
            ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);
        return claimsIdentity;
    }
}
