using colekta_api.Services.Category;
using Microsoft.AspNetCore.Mvc;

namespace colekta_api.Endpoints;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/categories")
            .WithTags("Categories");
        
        group.MapGet("", async (ICategoryInterface categoryInterface, int Page = 1, int PageSize = 12) =>
        {
            var result = await categoryInterface.GetAllCategories(Page, PageSize);
            return result;
        }).WithName("GetAllCategories")
        .WithSummary("Lista todas as categorias")
        .WithDescription("Retorna uma lista paginada de todas as categorias disponíveis na plataforma.");
    }
}