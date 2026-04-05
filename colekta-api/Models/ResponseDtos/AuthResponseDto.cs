using colekta_api.Models.Entities;

namespace colekta_api.Models.ResponseDtos;

public class AuthResponseDto
{
    public string Id { get; set; }
    public string NomeCompleto { get; set; }
    public string UserName { get; set; }
    public List<string>? Roles { get; set; }
    public string cpf { get; set; }
    public string Email { get; set; }
    
    public static AuthResponseDto ToAuthResponseDto(ApplicationUserModel user)
    {
        return new AuthResponseDto
        {
            Id = user.Id,
            NomeCompleto = user.FullName,
            UserName = user.UserName ?? string.Empty,
            cpf = user.Cpf ?? string.Empty,
            Email = user.Email ?? string.Empty
        };
    }
}