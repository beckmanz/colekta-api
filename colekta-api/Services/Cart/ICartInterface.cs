using System.Security.Claims;

namespace colekta_api.Services.Cart;

public interface ICartInterface
{
    Task<IResult> GetUserCartAsync(ClaimsPrincipal user);
}