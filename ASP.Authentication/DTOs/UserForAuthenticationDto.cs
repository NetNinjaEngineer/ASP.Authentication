using System.ComponentModel.DataAnnotations;

namespace ASP.Authentication.DTOs;

public class UserForAuthenticationDto
{
    [Required]
    public string? Email { get; set; }

    [Required]
    public string? Password { get; set; }
}
