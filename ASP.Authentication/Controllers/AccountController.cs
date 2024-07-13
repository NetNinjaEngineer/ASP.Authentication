using ASP.Authentication.DTOs;
using ASP.Authentication.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASP.Authentication.Controllers;
[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly JwtHandler _jwtHandler;

    public AccountController(UserManager<User> userManager, IMapper mapper, JwtHandler jwtHandler)
    {
        _userManager = userManager;
        _mapper = mapper;
        _jwtHandler = jwtHandler;
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegisterationResponseDto>> RegisterUser(
        [FromBody] UserForRegisterationDto model)
    {
        var user = _mapper.Map<User>(model);
        var result = await _userManager.CreateAsync(user, model.Password!);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);
            return BadRequest(new RegisterationResponseDto(false, errors.ToList()));
        }

        await _userManager.AddToRoleAsync(user, "Visitor");

        return Ok(new RegisterationResponseDto(true));

    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationResponseDto>> AuthenticateUser(
        [FromBody] UserForAuthenticationDto model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email!);
        if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password!))
            return Unauthorized(new AuthenticationResponseDto(false, "Invalid Authentication"));
        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwtHandler.CreateToken(user, roles);
        return Ok(new AuthenticationResponseDto(true, token));
    }

}
