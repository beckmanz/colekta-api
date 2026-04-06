namespace colekta_api.Models.FiltersDto;

public record ProductFilterDto(
    string? SearchTerm = null,
    decimal? MinPrice = null,
    decimal? MaxPrice = null,
    string? CategoryId = null,
    string? SortBy = null,
    int Page = 1,
    int PageSize = 12
    );