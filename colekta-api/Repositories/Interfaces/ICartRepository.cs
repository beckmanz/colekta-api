using colekta_api.Models.Entities;

namespace colekta_api.Repositories.Interfaces;

public interface ICartRepository
{
    Task<CartModel> GetCartByUserIdAsync(string userId);
}