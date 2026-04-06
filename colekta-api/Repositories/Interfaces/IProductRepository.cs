using colekta_api.Models.Entities;
using colekta_api.Models.FiltersDto;
using colekta_api.Models.ResponseDtos;

namespace colekta_api.Repositories.Interfaces;

public interface IProductRepository
{
    Task<PagedResponseDto<ProductModel>> GetAllProductsAsync(ProductFilterDto filters);
}