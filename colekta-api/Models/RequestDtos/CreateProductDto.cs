using colekta_api.Models.Entities;

namespace colekta_api.Models.RequestDtos;

/// <summary>
/// DTO para criação de um novo produto, contendo os dados necessários para registrar um produto no sistema.
/// </summary>
public class CreateProductDto
{
    /// <summary>
    /// Nome do produto.
    /// </summary>
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// Descrição detalhada do produto.
    /// </summary>
    public string Descricao { get; set; } = string.Empty;

    /// <summary>
    /// Preço do produto em reais.
    /// </summary>
    public decimal Preco { get; set; }

    /// <summary>
    /// Quantidade em estoque do produto.
    /// </summary>
    public int Estoque { get; set; }

    /// <summary>
    /// ID da categoria à qual o produto pertence.
    /// </summary>
    public string CategoriaId { get; set; } = string.Empty;

    /// <summary>
    /// Estado de conservação do produto (ex: Novo, Usado).
    /// </summary>
    public string EstadoConservacao { get; set; } = string.Empty;

    /// <summary>
    /// Coleção de imagens do produto enviadas via formulário.
    /// </summary>
    public IFormFileCollection Imagens { get; set; }

    /// <summary>
    /// Índice da imagem que será usada como capa do produto (baseado na coleção de imagens).
    /// </summary>
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
