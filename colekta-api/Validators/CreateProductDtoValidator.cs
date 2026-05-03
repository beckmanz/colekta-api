using FluentValidation;
using colekta_api.Models.RequestDtos;

namespace colekta_api.Validators;

/// <summary>
/// Validador para CreateProductDto usando FluentValidation.
/// </summary>
public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductDtoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome do produto é obrigatório.")
            .Length(1, 100).WithMessage("O nome deve ter entre 1 e 100 caracteres.");

        RuleFor(x => x.Descricao)
            .NotEmpty().WithMessage("A descrição do produto é obrigatória.")
            .MaximumLength(500).WithMessage("A descrição deve ter no máximo 500 caracteres.");

        RuleFor(x => x.Preco)
            .GreaterThan(0).WithMessage("O preço deve ser maior que zero.");

        RuleFor(x => x.Estoque)
            .GreaterThanOrEqualTo(0).WithMessage("O estoque deve ser maior ou igual a zero.");

        RuleFor(x => x.CategoriaId)
            .NotEmpty().WithMessage("O ID da categoria é obrigatório.");

        RuleFor(x => x.EstadoConservacao)
            .NotEmpty().WithMessage("O estado de conservação é obrigatório.");

        RuleFor(p => p.Imagens)
            .NotEmpty().WithMessage("O produto precisa de pelo menos uma imagem.")
            .Must(imgs => imgs.Count <= 5).WithMessage("Você pode enviar no máximo 5 imagens.")
            .ForEach(imagem => {
                imagem.ChildRules(img => {
                    img.RuleFor(i => i.Length)
                        .LessThanOrEqualTo(3 * 1024 * 1024)
                        .WithMessage("Cada imagem deve ter no máximo 3MB.");
                    
                    img.RuleFor(i => i.ContentType)
                        .Must(ct => ct.Equals("image/jpeg") || ct.Equals("image/png") || ct.Equals("image/webp"))
                        .WithMessage("Apenas formatos JPEG, PNG ou WEBP são aceitos.");
                });
            });
        
        RuleFor(p => p.IndexImagemCapa)
            .Must((dto, index) => index >= 0 && index < dto.Imagens.Count)
            .WithMessage("O índice da imagem de capa é inválido para a quantidade de fotos enviadas.");
    }
}
