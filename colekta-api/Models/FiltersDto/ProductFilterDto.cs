namespace colekta_api.Models.FiltersDto;

/// <summary>
/// DTO para filtros de produtos, usado para paginar e filtrar listas de produtos.
/// </summary>
/// <param name="SearchTerm">Termo de busca para filtrar produtos por nome ou descrição (opcional).</param>
/// <param name="MinPrice">Preço mínimo para filtrar produtos (opcional).</param>
/// <param name="MaxPrice">Preço máximo para filtrar produtos (opcional).</param>
/// <param name="CategoryId">ID da categoria para filtrar produtos (opcional).</param>
/// <param name="SortBy">Campo para ordenação (ex: "price_asc", "price_desc") (opcional).</param>
/// <param name="Page">Número da página para paginação (padrão: 1).</param>
/// <param name="PageSize">Número de itens por página (padrão: 12).</param>
public record ProductFilterDto(
    string? SearchTerm = null,
    decimal? MinPrice = null,
    decimal? MaxPrice = null,
    string? CategoryId = null,
    string? SortBy = null,
    int Page = 1,
    int PageSize = 12
    );