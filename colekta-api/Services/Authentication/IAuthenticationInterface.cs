using System.Security.Claims;
using colekta_api.Models.RequestDtos;
using colekta_api.Models.ResponseDtos;

namespace colekta_api.Services.Authentication;

public interface IAuthenticationInterface
{
    Task<(IResult Result, string? Token)> RegisterUserAsync(RegisterDto registerDto);
    Task<(IResult Result, string? Token)> LoginUserAsync(LoginDto loginDto);
    Task<IResult> GetCurrentUserAsync(ClaimsPrincipal userClaims);
}