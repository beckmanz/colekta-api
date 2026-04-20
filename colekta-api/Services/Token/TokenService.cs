using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using colekta_api.Models.Entities;
using Microsoft.IdentityModel.Tokens;

namespace colekta_api.Services.Token;

public class TokenService : ITokenInterface
{
    private readonly IConfiguration _config;

    public TokenService(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateJwtToken(ApplicationUserModel user, List<string> roles)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("name", user.FullName)
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim("Role", role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public void SetCookieTokenJwt(HttpContext httpContext, string token)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddHours(8)
        };

        httpContext.Response.Cookies.Append("ColektaAccessToken", token, cookieOptions);
    }
}