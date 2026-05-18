using System.Security.Claims;
using colekta_api.Services.Cart;

namespace colekta_api.Endpoints;

public static class CartEndpoints
{
    public static void MapCartEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/cart")
            .WithTags("Cart")
            .RequireAuthorization();

        group.MapGet("", async (ICartInterface cartInterface,
            ClaimsPrincipal userClaims) =>
        {
            var result = await cartInterface.GetUserCartAsync(userClaims);
            return result;
        }).WithName("GetUserCart")
        .WithSummary("Retorna o carrinho do usuário autenticado")
        .WithDescription("Retorna o carrinho de compras do usuário atualmente autenticado, incluindo os itens adicionados, quantidades e preços totais.");
    }
}