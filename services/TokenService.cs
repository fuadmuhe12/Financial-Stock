using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.Interfaces;
using api.Models;
using Microsoft.IdentityModel.Tokens;

namespace api.services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    private readonly SymmetricSecurityKey _key;

    public TokenService(IConfiguration config)
    {
        _config = config;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]!));
    }

    public string CreateToken(AppUser appUser)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.GivenName, appUser.Name),
            new Claim(JwtRegisteredClaimNames.Email, appUser.Email!), 
            new Claim(JwtRegisteredClaimNames.Sub, appUser.Id),
            new Claim(ClaimTypes.Role, appUser.Roles)
        };

        var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(3),
            signingCredentials: cred,
            issuer: _config["JWT:Issuer"],
            audience: _config["JWT:Audience"]
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
