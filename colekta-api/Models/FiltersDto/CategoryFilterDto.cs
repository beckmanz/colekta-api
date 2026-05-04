namespace colekta_api.Models.FiltersDto;

/// <summary>
/// DTO para filtros de categorias, permitindo busca por termo de pesquisa e paginação.
/// </summary>
/// <param name="SearchTerm">Termo de pesquisa opcional para filtrar categorias pelo nome.</param>
/// <param name="Page">Número da página para paginação (padrão: 1).</param>
/// <param name="PageSize">Número de itens por página (padrão: 12).</param>
public record CategoryFilterDto(string? SearchTerm = null, int Page = 1, int PageSize = 12);