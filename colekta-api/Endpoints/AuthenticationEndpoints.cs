using System.Security.Claims;
using colekta_api.Models.RequestDtos;
using colekta_api.Services.Authentication;
using colekta_api.Services.Token;
using Microsoft.AspNetCore.Mvc;

namespace colekta_api.Endpoints;

public static class AuthenticationEndpoints
{
    public static void MapAuthenticationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth")
            .WithTags("Authentication");
        
        group.MapPost("register", async (
            [FromBody] RegisterDto request,
            IAuthenticationInterface authInterface,
            HttpContext httpContext,
            ITokenInterface tokenInterface) =>
        {
            var (result, token) = await authInterface.RegisterUserAsync(request);
            if (token is not null)
            {
                tokenInterface.SetCookieTokenJwt(httpContext, token);
            }
            return result;
        }).WithName("Register")
        .WithSummary("Registra um novo usuário")
        .WithDescription("Cria um novo usuário na plataforma e retorna um token de autenticação via cookie.");
        
        group.MapPost("login", async (
            [FromBody] LoginDto request,
            IAuthenticationInterface authInterface,
            HttpContext httpContext,
            ITokenInterface tokenInterface) =>
        {
            var (result, token) = await authInterface.LoginUserAsync(request);
            if (token is not null)
            {
                tokenInterface.SetCookieTokenJwt(httpContext, token);
            }
            return result;
        }).WithName("Login")
        .WithSummary("Loga um usuário")
        .WithDescription("Faz o login de um usuário na plataforma e retorna um token de autenticação via cookie.");

        group.MapGet("me", async (
            IAuthenticationInterface authInterface,
            ClaimsPrincipal userClaims) =>
        {
            var result = await authInterface.GetCurrentUserAsync(userClaims);
            return result;
        }).WithName("Me")
        .WithSummary("Retorna as informações do usuário autenticado")
        .WithDescription("Retorna as informações do usuário atualmente autenticado com base no token de autenticação presente no cookie.");
        
        group.MapPost("logout", (HttpContext httpContext) =>
        {
            httpContext.Response.Cookies.Delete("ColektaAccessToken");
            return Results.Ok(new { Message = "Logout successful" });
        }).WithName("Logout")
        .WithSummary("Realiza logout do usuário")
        .WithDescription("Remove o cookie de autenticação para efetuar o logout do usuário.");
    }
}