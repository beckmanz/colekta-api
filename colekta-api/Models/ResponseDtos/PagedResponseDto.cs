namespace colekta_api.Models.ResponseDtos;

public record PagedResponseDto<T>(
    IEnumerable<T> Items,
    int TotalItems,
    int CurrentPage,
    int TotalPages
);