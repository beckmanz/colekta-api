using colekta_api.Models.FiltersDto;
using colekta_api.Services.Category;
using Microsoft.AspNetCore.Mvc;

namespace colekta_api.Endpoints;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/categories")
            .WithTags("Categories");
        
        group.MapGet("", async (ICategoryInterface categoryInterface, [AsParameters] CategoryFilterDto filter) =>
        {
            var result = await categoryInterface.GetAllCategories(filter);
            return result;
        }).WithName("GetAllCategories")
        .WithSummary("Lista todas as categorias")
        .WithDescription("Retorna uma lista paginada de todas as categorias disponíveis na plataforma.");
    }
}