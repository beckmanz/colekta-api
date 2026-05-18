namespace colekta_api.Models.ResponseDtos;

public record CartItemResponseDto(
    Guid ProductId,
    string Nome,
    decimal PrecoAtual,
    int QuantidadeSolicitada,
    int EstoqueDisponivel,
    string ImagemCapaUrl,
    bool IsDisponivel
);