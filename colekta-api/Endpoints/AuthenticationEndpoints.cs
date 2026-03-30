using colekta_api.Models.RequestDtos;
using colekta_api.Services.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace colekta_api.Endpoints;

public static class AuthenticationEndpoints
{
    public static void MapAuthenticationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth").WithTags("Authentication");
        
        group.MapPost("register", async (
            [FromBody] RegisterDto request,
            IAuthenticationInterface authInterface,
            HttpContext httpContext) =>
        {
            var (result, token) = await authInterface.RegisterUserAsync(request);
            if (token is not null)
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddHours(12)
                };

                httpContext.Response.Cookies.Append("ColektaAccessToken", token, cookieOptions);

            }
            return result;
        }).WithName("Register");
    }
}