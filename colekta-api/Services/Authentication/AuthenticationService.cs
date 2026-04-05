using colekta_api.Helpers;
using colekta_api.Models.Entities;
using colekta_api.Models.RequestDtos;
using colekta_api.Models.ResponseDtos;
using colekta_api.Models.ResultsModel;
using colekta_api.Services.Token;
using Microsoft.AspNetCore.Identity;

namespace colekta_api.Services.Authentication;

public class AuthenticationService : IAuthenticationInterface
{
    private readonly ITokenInterface _tokenService;
    private readonly UserManager<ApplicationUserModel> _userManager;

    public AuthenticationService(ITokenInterface tokenService, UserManager<ApplicationUserModel> userManager)
    {
        _tokenService = tokenService;
        _userManager = userManager;
    }

    public async Task<(IResult Result, string? Token)> RegisterUserAsync(RegisterDto registerDto)
    {
        if (!ValidationUtils.IsValidEmail(registerDto.Email))
        {
            return ("O formato do e-mail é inválido.".ToBadRequestResult(), null);
        }
        var existEmail = await _userManager.FindByEmailAsync(registerDto.Email);
        if (existEmail != null)
        {
            return ("Email já registrado!!".ToConflictResult(), null);
        }

        var userName = ValidationUtils.GenerateUserName(registerDto.NomeCompleto);
        Boolean genSlug = true;

        while (genSlug)
        {
            var slug = await _userManager.FindByNameAsync(userName);
            if (slug is not null)
            {
                userName = ValidationUtils.GenerateUserName(registerDto.NomeCompleto); 
            }
            else
            {
                genSlug = false;
            }
        }
        
        var user = new ApplicationUserModel()
        {
            UserName = userName,
            Email = registerDto.Email,
            FullName = registerDto.NomeCompleto,
        };
        
        var result = await _userManager.CreateAsync(user, registerDto.Senha);
        
        if (!result.Succeeded)
        {
            return (result.ToUnprocessableEntityResult(), null);
        }
        
        await _userManager.AddToRoleAsync(user, "Cliente");
        var roles = new List<string> { "Cliente" };
        var token = _tokenService.GenerateJwtToken(user, roles);
        var response = AuthResponseDto.ToAuthResponseDto(user);
        response.Roles = roles;
        
        var httpResult = response.ToCreatedResult(
            location: "/api/Authentication/me",
            message: "Usuário registrado com sucesso!");

        return (httpResult, token);
    }

    public async Task<(IResult Result, string? Token)> LoginUserAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user is null)
        {
            return ("Credenciais inválidas.".ToUnauthorizedResult(), null);
        }
        
        if(!_userManager.CheckPasswordAsync(user, loginDto.Senha).Result)
        {
            return ("Credenciais inválidas.".ToUnauthorizedResult(), null);
        }

        var response = AuthResponseDto.ToAuthResponseDto(user);
        var userRoles = await _userManager.GetRolesAsync(user);
        response.Roles = userRoles.ToList();
        
        var httpResult = response.ToOkResult("Login realizado com sucesso!");
        var token = _tokenService.GenerateJwtToken(user, userRoles.ToList());
        return (httpResult, token);
    }
}