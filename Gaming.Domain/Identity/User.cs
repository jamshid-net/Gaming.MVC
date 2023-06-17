
using Microsoft.AspNetCore.Identity;

namespace Gaming.Domain.Identity;

public class User: IdentityUser
{
    public DateOnly? BirthDate { get; set; }
    public string? ProfilePicture { get; set; }


}
