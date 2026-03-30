using colekta_api.Models.Entities;

namespace colekta_api.Services.Token;

public interface ITokenInterface
{ 
    string GenerateJwtToken(ApplicationUserModel user, List<string> roles);
}