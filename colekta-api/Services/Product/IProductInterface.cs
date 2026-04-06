using colekta_api.Models.FiltersDto;

namespace colekta_api.Services.Product;

public interface IProductInterface
{
    Task<IResult> GetAllProductAsync(ProductFilterDto filter);
}