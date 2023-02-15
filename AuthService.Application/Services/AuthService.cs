using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthService.Application.Abstractions.Services;
using AuthService.Application.Models;
using AuthService.Application.Settings;
using AuthService.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Application.Services;

public class AuthService : IAuthService
{
    private readonly JwtOptions _jwtSetting;
    
    public AuthService(IOptions<JwtOptions> jwtSettingOptions)
    {
        _jwtSetting = jwtSettingOptions.Value;
    }
    public Token GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email,user.Email),
        };
        var expires = DateTime.Now.AddDays(1);
        var token = new JwtSecurityToken(_jwtSetting.Issuer,
            _jwtSetting.Audience,
            claims,
            expires: expires,
            signingCredentials: credentials);
        
        return new Token
        {
            Scheme = "Bearer",
            Value =  new JwtSecurityTokenHandler().WriteToken(token),
            Expires = expires.ToString()
        };
    }
}