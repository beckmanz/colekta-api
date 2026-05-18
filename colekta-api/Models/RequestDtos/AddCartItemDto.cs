namespace colekta_api.Models.RequestDtos;

public record AddCartItemDto(
    Guid ProductId,
    int Quantidade
);