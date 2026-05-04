using System.Security.Claims;
using colekta_api.Models.FiltersDto;
using colekta_api.Models.RequestDtos;

namespace colekta_api.Services.Product;

public interface IProductInterface
{
    Task<IResult> GetAllProductAsync(ProductFilterDto filter);
    Task<IResult> CreateProductAsync(CreateProductDto dto, ClaimsPrincipal userClaims);
    Task<IResult> GetProductById(Guid Id);
}