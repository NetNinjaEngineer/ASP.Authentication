using System.ComponentModel.DataAnnotations;

namespace ASP.Authentication.DTOs;

public class UserForRegisterationDto
{
    [Required(ErrorMessage = "FirstName is Required.")]
    public string? FirstName { get; set; }


    [Required(ErrorMessage = "LastName is Required.")]
    public string? LastName { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required(ErrorMessage = "The password is Required.")]
    public string? Password { get; set; }

    [Required]
    [Compare("Password", ErrorMessage = "Password and Confirm password do not match.")]
    public string? ConfirmPassword { get; set; }
}
