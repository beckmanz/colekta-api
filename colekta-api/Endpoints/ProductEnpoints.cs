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

        group.MapGet("/", async (IProductInterface productService, ClaimsPrincipal user, [AsParameters] ProductFilterDto filter) =>
            {
                var result = await productService.GetAllProductAsync(filter, user);
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

        group.MapGet("/{Id}", async (IProductInterface productService, Guid Id) =>
        {
            var result = await productService.GetProductById(Id);
            return result;
        }).DisableAntiforgery()
            .WithName("GetProductById")
            .WithSummary("Retorna um produto por ID")
            .WithDescription(
            "Retorna um produto com base no ID fornecido. O ID deve ser um GUID válido.")
            .AllowAnonymous();

        group.MapPut("/{id:guid}", async (
                Guid id,
                UpdateProductDto dto,
                ClaimsPrincipal user,
                IProductInterface productService) =>
            {
                var result = await productService.UpdateProductAsync(id, dto, user);
                return result;
            })
            .AddEndpointFilter<ValidationFilter<UpdateProductDto>>()
            .WithName("UpdateProduct")
            .WithSummary("Atualizar um produto")
            .WithDescription(
                "Atualiza um produto existente com base no ID fornecido.");

        group.MapDelete("/{id:guid}", async (
                Guid id,
                ClaimsPrincipal user,
                IProductInterface productService) =>
            {
                var result = await productService.SoftDeleteProductAsync(id, user);
                return result;
            }).DisableAntiforgery()
            .WithName("DeleteProduct")
            .WithSummary("Remover um produto")
            .WithDescription(
                "Realiza a exclusão lógica de um produto com base no ID fornecido. O produto não será removido fisicamente do banco de dados, mas será marcado como excluído.");

    }
}