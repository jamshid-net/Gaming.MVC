using Gaming.Application.Common.Interfaces;
using System.Security.Claims;

namespace Gaming.Application.Common.Services;

public class CurrentUserService:ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

    public string? UserProfilePicture => _httpContextAccessor.HttpContext?.User?.FindFirstValue("ProfilePicture");
}
