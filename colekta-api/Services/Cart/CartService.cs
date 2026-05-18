using System.Security.Claims;
using colekta_api.Models.ResponseDtos;
using colekta_api.Models.ResultsModel;
using colekta_api.Repositories.Interfaces;

namespace colekta_api.Services.Cart;

public class CartService : ICartInterface
{
    private readonly ICartRepository _cartRepository;

    public CartService(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<IResult> GetUserCartAsync(ClaimsPrincipal user)
    {
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId is null)
        {
            return "".ToUnauthorizedResult();
        }
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);

        var response = CartResponseDto.ToDto(cart);
        return response.ToOkResult("Carrinho recuperado com sucesso");
    }
}