using System.Security.Claims;
using colekta_api.Data;
using colekta_api.Models.Entities;
using colekta_api.Repositories.Interfaces;
using colekta_api.Services.Cart;
using Microsoft.EntityFrameworkCore;

namespace colekta_api.Repositories;

public class CartRepository : ICartRepository
{
    private readonly ApplicationDbContext _context;

    public CartRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CartModel> GetCartByUserIdAsync(string userId)
    {
        var result = await _context.Carts
            .Include(c => c.Items)
            .ThenInclude(i => i.Product)
            .ThenInclude(p => p.Images)
            .Where(c => c.UserId == userId)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        
        return result;
    }
}