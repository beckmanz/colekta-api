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

    public IQueryable<ProductModel> GetProductsQuery(ProductFilterDto filters)
    {
        var query = _context.Products
            .Include( p => p.Images)
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filters.SearchTerm))
        {
            var search = filters.SearchTerm.ToLower();
            query = query.Where(p => p.Name.ToLower().Contains(search) 
                                     || p.Description.ToLower().Contains(search));
        }
    
        if (filters.MinPrice.HasValue)
            query = query.Where(p => p.Price >= filters.MinPrice.Value);
    
        if (filters.MaxPrice.HasValue)
            query = query.Where(p => p.Price <= filters.MaxPrice.Value);

        return filters.SortBy switch
        {
            "price_asc" => query.OrderBy(p => p.Price),
            "price_desc" => query.OrderByDescending(p => p.Price),
            _ => query.OrderBy(p => p.Name)
        };
    }
    public Task<ProductModel> CreateProductAsync(ProductModel product)
    {
        _context.Products.Add(product);
        return _context.SaveChangesAsync()
            .ContinueWith(t => product);
    }
}