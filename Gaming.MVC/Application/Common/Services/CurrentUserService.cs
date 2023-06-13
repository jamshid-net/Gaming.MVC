using Gaming.MVC.Application.Common.Interfaces;
using System.Security.Claims;

namespace Gaming.MVC.Application.Common.Services;

public class CurrentUserService:ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IApplicationDbContext _applicationDbContext;

    public CurrentUserService(IHttpContextAccessor httpcontextAccessor, IApplicationDbContext applicationDbContext)
          => (_httpContextAccessor, _applicationDbContext) = (httpcontextAccessor, applicationDbContext);
    public string? UserName => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
    public int? UserId => _applicationDbContext?.Users?.SingleOrDefault(x => x.UserName == this.UserName)?.UserId;
}
