using colekta_api.Data;
using colekta_api.Models.Entities;
using colekta_api.Models.FiltersDto;
using colekta_api.Models.ResponseDtos;
using colekta_api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace colekta_api.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResponseDto<ProductModel>> GetAllProductsAsync(ProductFilterDto filters)
    {
        var query = _context.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filters.SearchTerm))
        {
            var search = filters.SearchTerm.ToLower();
            query = query.Where(p => p.Name.ToLower().Contains(search) 
                                     || p.Description.Contains(filters.SearchTerm));
        }
        
        if(filters.MinPrice.HasValue)
        {
            query = query.Where(p => p.Price >= filters.MinPrice.Value);
        }
        
        if(filters.MaxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= filters.MaxPrice.Value);
        }
        
        query = filters.SortBy switch
        {
            "price_asc" => query.OrderBy(p => p.Price),
            "price_desc" => query.OrderByDescending(p => p.Price),
            _ => query.OrderBy(p => p.Name)
        };
        
        var totalItems = await query.CountAsync();
        
        var itemsToSkip = (filters.Page - 1) * filters.PageSize;
        var items = await query.Skip(itemsToSkip)
            .Take(filters.PageSize)
            .ToListAsync();
        
        var totalPages = (int)Math.Ceiling((double)totalItems / filters.PageSize);
        
        return new PagedResponseDto<ProductModel>(
            Items: items,
            TotalItems: totalItems,
            CurrentPage: filters.Page,
            TotalPages: totalPages
        );
    }
}