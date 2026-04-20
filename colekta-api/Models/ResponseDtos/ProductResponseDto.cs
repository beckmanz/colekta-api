using colekta_api.Models.Entities;

namespace colekta_api.Models.ResponseDtos;

public class ProductResponseDto()
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public int Estoque { get; set; }
    public Guid CategoriaId { get; set; }
    public string SellerId { get; set; }
    public List<ProductImageResponseDto> Imagens { get; set; } = new List<ProductImageResponseDto>();

    public static ProductResponseDto ToDto(ProductModel product)
    {
        var response = new ProductResponseDto
        {
            Id = product.Id,
            Nome = product.Name,
            Descricao = product.Description,
            Estoque = product.Stock,
            Preco = product.Price,
            CategoriaId = product.CategoryId,
            SellerId = product.SellerId,
            Imagens = product.Images.Select(img => ProductImageResponseDto.ToDto(img)).ToList()
        };
        
        return response;
    }
}