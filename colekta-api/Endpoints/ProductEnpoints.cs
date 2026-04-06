using colekta_api.Models.FiltersDto;
using colekta_api.Services.Product;

namespace colekta_api.Endpoints;

public static class ProductEnpoints
{
    public static void MapProductEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/products")
            .WithTags("Products");
        
        group.MapGet("/", async (IProductInterface productService, [AsParameters] ProductFilterDto filter) =>
        {
            var result = await productService.GetAllProductAsync(filter);
            return result;
        }).WithName("Products")
        .WithSummary("Retorna uma lista de produtos")
        .WithDescription("Retorna uma lista de produtos com base nos filtros fornecidos. Os filtros podem incluir categoria, faixa de preço, nome, entre outros.");
        
        
    }
}