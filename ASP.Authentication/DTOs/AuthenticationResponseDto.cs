namespace ASP.Authentication.DTOs;

public class AuthenticationResponseDto(bool isAuthSuccessfull, string? token, string? errorMessage = null)
{
    public bool IsAuthSuccessfull { get; set; } = isAuthSuccessfull;
    public string? ErrorMessage { get; set; } = errorMessage;
    public string? Token { get; set; } = token;
}
