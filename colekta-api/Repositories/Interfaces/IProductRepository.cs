using colekta_api.Models.Entities;
using colekta_api.Models.FiltersDto;
using colekta_api.Models.ResponseDtos;

namespace colekta_api.Repositories.Interfaces;

public interface IProductRepository
{
    IQueryable<ProductModel> GetAllProductsQuery(ProductFilterDto filters, bool includeDeleted);
    Task<ProductModel> CreateProductAsync(ProductModel product);
    Task<ProductModel> GetProductByIdAsync(Guid Id);
}