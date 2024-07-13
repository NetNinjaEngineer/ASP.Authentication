namespace ASP.Authentication.DTOs;

public class RegisterationResponseDto
{
    public RegisterationResponseDto(bool isSuccessfullRegisteration, List<string>? errors = null)
    {
        IsSuccessfullRegisteration = isSuccessfullRegisteration;
        Errors = errors;
    }

    public bool IsSuccessfullRegisteration { get; set; }
    public List<string>? Errors { get; set; } = [];
}
