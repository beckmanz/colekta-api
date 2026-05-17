namespace colekta_api.Models.RequestDtos;

/// <summary>
/// DTO para edição de um produto, contendo os dados alteraveis um produto no sistema.
/// </summary>
public class UpdateProductDto
{
    /// <summary>
    /// Nome do produto.
    /// </summary>
    public string? Nome { get; set; } 
    
    /// <summary>
    /// Descrição detalhada do produto.
    /// </summary>
    public string? Descricao { get; set; }

    /// <summary>
    /// Preço do produto em reais.
    /// </summary>
    public decimal? Preco { get; set; }

    /// <summary>
    /// Quantidade em estoque do produto.
    /// </summary>
    public int? Estoque { get; set; }

    /// <summary>
    /// ID da categoria à qual o produto pertence.
    /// </summary>
    public string? CategoriaId { get; set; }
}