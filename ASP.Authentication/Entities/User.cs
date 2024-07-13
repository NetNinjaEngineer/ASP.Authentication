using Microsoft.AspNetCore.Identity;

namespace ASP.Authentication.Entities;

public class User : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
