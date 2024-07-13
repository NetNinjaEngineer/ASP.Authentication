using ASP.Authentication.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ASP.Authentication;

public class JwtHandler
{
    private readonly IConfiguration _configuration;
    private readonly IConfigurationSection _jwtSection;

    public JwtHandler(IConfiguration configuration)
    {
        _configuration = configuration;
        _jwtSection = _configuration.GetSection("JWT");
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(_jwtSection["Key"]!);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private List<Claim> GetClaims(User user, IList<string> roles)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.Name, user.UserName!)
        };

        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        return claims;
    }

    private JwtSecurityToken GenerateSecurityToken(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var securityToken = new JwtSecurityToken(
            issuer: _jwtSection["ValidIssuer"],
            audience: _jwtSection["ValidAudience"],
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.Now.AddDays(Convert.ToDouble(_jwtSection["DurationInDays"]))
            );

        return securityToken;

    }

    public string CreateToken(User user, IList<string> roles)
    {
        var claims = GetClaims(user, roles);
        var signingCredientials = GetSigningCredentials();
        var securityToken = GenerateSecurityToken(signingCredientials, claims);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

}
