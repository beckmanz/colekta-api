using colekta_api.Models.FiltersDto;
using colekta_api.Services.Category;
using Microsoft.OpenApi;

namespace colekta_api.Endpoints;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/categories")
            .WithTags("Categories")
            .RequireAuthorization("ElevatedRights");
        
        group.MapGet("", async (ICategoryInterface categoryInterface, [AsParameters] CategoryFilterDto filter) =>
        {
            var result = await categoryInterface.GetAllCategories(filter);
            return result;
        }).WithName("GetAllCategories")
        .AllowAnonymous()
        .WithSummary("Lista todas as categorias")
        .WithDescription("Retorna uma lista paginada de todas as categorias disponíveis na plataforma.");
        
        group.MapPost("", async (ICategoryInterface categoryInterface, string nome) =>
        {
            var result = await categoryInterface.CreateCategoryAsync(nome);
            return result;
        }).WithName("CreateCategory")
        .WithTags("Categories")
        .WithSummary("Cria uma nova categoria")
        .WithDescription("Cria uma nova categoria com base no nome fornecido. O nome deve ser único e não pode estar vazio. Retorna a categoria criada ou um erro se a categoria já existir.");
    }
}