using colekta_api.Models.FiltersDto;
using colekta_api.Models.ResultsModel;
using colekta_api.Repositories.Interfaces;

namespace colekta_api.Services.Product;

public class ProductService : IProductInterface
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IResult> GetAllProductAsync(ProductFilterDto filter)
    {
        var products = await _productRepository.GetAllProductsAsync(filter);
        
        return products.ToOkResult();
    }
}