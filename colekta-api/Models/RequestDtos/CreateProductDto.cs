using colekta_api.Models.Entities;

namespace colekta_api.Models.RequestDtos;

public class CreateProductDto
{
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public int Estoque { get; set; }
    public string CategoriaId { get; set; } = string.Empty;
    public string EstadoConservacao { get; set; } = string.Empty;
    public IFormFileCollection Imagens { get; set; }
    public int IndexImagemCapa { get; set; }

    public static ProductModel ToProductModel(CreateProductDto dto, CategoryModel category, ApplicationUserModel seller)
    {

        var product = new ProductModel
        {
            Name = dto.Nome,
            Description = dto.Descricao,
            Price = dto.Preco,
            Stock = dto.Estoque,
            Condition = dto.EstadoConservacao,
            CategoryId = category.Id,
            SellerId = seller.Id,
        };

        return product;
    }
}
