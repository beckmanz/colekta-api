namespace colekta_api.Models.FiltersDto;

public record CategoryFilterDto(string? SearchTerm = null, int Page = 1, int PageSize = 12);