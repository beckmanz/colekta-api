using colekta_api.Models.Entities;
using colekta_api.Models.FiltersDto;
using colekta_api.Models.ResponseDtos;

namespace colekta_api.Repositories.Interfaces;

public interface IProductRepository
{
    IQueryable<ProductModel> GetProductsQuery(ProductFilterDto filters);
    Task<ProductModel> CreateProductAsync(ProductModel product);
}