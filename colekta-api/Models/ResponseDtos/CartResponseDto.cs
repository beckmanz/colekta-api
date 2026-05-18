using colekta_api.Models.Entities;

namespace colekta_api.Models.ResponseDtos;

public class CartResponseDto()
{
    
    public List<CartItemResponseDto> Itens { get; set; } = new List<CartItemResponseDto>();
    public decimal ValorTotal { get; set; }
    
    public static CartResponseDto ToDto(CartModel? cart)
    {
        if (cart == null || cart.Items == null || !cart.Items.Any())
        {
            return new CartResponseDto()
            {
                Itens = new List<CartItemResponseDto>(),
                ValorTotal = 0
            };
        }

        var itensDto = cart.Items
            .Where(item => item.Product != null && !item.Product.IsDelete)
            .Select(item => 
            {
                var produto = item.Product;
                bool isDisponivel = produto.Stock >= item.Quantity;

                var capaUrl = produto.Images?.FirstOrDefault(i => i.IsCover)?.Url;

                return new CartItemResponseDto(
                    ProductId: produto.Id,
                    Nome: produto.Name,
                    PrecoAtual: produto.Price,
                    QuantidadeSolicitada: item.Quantity,
                    EstoqueDisponivel: produto.Stock,
                    ImagemCapaUrl: capaUrl,
                    IsDisponivel: isDisponivel
                );
            }).ToList();

        var valorTotal = itensDto
            .Where(i => i.IsDisponivel)
            .Sum(i => i.PrecoAtual * i.QuantidadeSolicitada);

        return new CartResponseDto()
        {
            Itens = itensDto,
            ValorTotal = valorTotal
        };
    }
}