using System.Security.Claims;
using colekta_api.Models.RequestDtos;

namespace colekta_api.Services.Cart;

public interface ICartInterface
{
    Task<IResult> GetUserCartAsync(ClaimsPrincipal user);
    Task<IResult> AddItemToCartAsync(ClaimsPrincipal user, AddCartItemDto dto);
}