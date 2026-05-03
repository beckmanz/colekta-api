using System.Security.Claims;
using colekta_api.Filters;
using colekta_api.Models.FiltersDto;
using colekta_api.Models.RequestDtos;
using colekta_api.Services.Product;
using Microsoft.AspNetCore.Mvc;

namespace colekta_api.Endpoints;

public static class ProductEnpoints
{
    public static void MapProductEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/products")
            .WithTags("Products")
            .RequireAuthorization("Common");

        group.MapGet("/", async (IProductInterface productService, [AsParameters] ProductFilterDto filter) =>
            {
                var result = await productService.GetAllProductAsync(filter);
                return result;
            }).WithName("Products")
            .WithSummary("Retorna uma lista de produtos")
            .WithDescription(
                "Retorna uma lista de produtos com base nos filtros fornecidos. Os filtros podem incluir categoria, faixa de preço, nome, entre outros.")
            .AllowAnonymous();

        group.MapPost("/", async (IProductInterface productService, ClaimsPrincipal user, [FromForm]CreateProductDto product) =>
            {
                var result = await productService.CreateProductAsync(product, user);
                return result;
            }).DisableAntiforgery()
            .AddEndpointFilter<MultipartValidationFilter<CreateProductDto>>()
            .WithName("CreateProduct")
            .WithSummary("Cria um novo produto")
            .WithDescription(
                "Cria um novo produto com base nos dados fornecidos. O usuário deve ser autenticado e ter a função de 'Admin' ou 'Vendedor' para criar um produto.");
        
    }
}