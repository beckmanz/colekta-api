using System.Security.Claims;
using colekta_api.Models.Entities;
using colekta_api.Models.RequestDtos;
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

    public async Task<IResult> AddItemToCartAsync(ClaimsPrincipal user, AddCartItemDto dto)
    {
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId is null)
        {
            return "".ToUnauthorizedResult();
        }
        var product = await _cartRepository.GetProductForCartAsync(dto.ProductId);
        if (product == null)
        {
            return "O produto informado não está disponível para compra.".ToNotFoundResult();
        }
        
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        bool isNewCart = false;

        if (cart == null)
        {
            isNewCart = true;
            cart = new CartModel
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Items = new List<CartItemModel>()
            };
        }
        
        var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == dto.ProductId);
        int novaQuantidade = dto.Quantidade;

        if (existingItem != null)
        {
            novaQuantidade += existingItem.Quantity;
        }

        if (novaQuantidade > product.Stock)
        {
            return "Estoque insuficiente para a quantidade solicitada.".ToBadRequestResult();
        }
        
        if (existingItem != null)
        {
            existingItem.Quantity = novaQuantidade;
        }
        else
        {
            cart.Items.Add(new CartItemModel
            {
                Id = Guid.NewGuid(),
                CartId = cart.Id,
                ProductId = dto.ProductId,
                Quantity = dto.Quantidade
            });
        }
        
        if (isNewCart)
        {
            await _cartRepository.CreateCartAsync(cart);
        }
        else
        {
            await _cartRepository.UpdateCartAsync(cart);
        }
        
        var updatedCart = await _cartRepository.GetCartByUserIdAsync(userId);
        return CartResponseDto.ToDto(updatedCart).ToOkResult("Produto adicionado ao carrinho com sucesso");
    }
}