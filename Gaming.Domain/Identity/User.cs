
using Microsoft.AspNetCore.Identity;

namespace Gaming.Domain.Identity;

public class User: IdentityUser
{
    public string FullName { get; set; }

}
