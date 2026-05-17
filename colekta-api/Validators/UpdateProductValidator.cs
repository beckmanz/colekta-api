using colekta_api.Models.RequestDtos;
using FluentValidation;

namespace colekta_api.Validators;

public class UpdateProductValidator : AbstractValidator<UpdateProductDto>
{
    public UpdateProductValidator()
    {
        RuleFor(p => p.Nome)
            .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.")
            .When(p => p.Nome != null);

        RuleFor(p => p.Preco)
            .GreaterThan(0).WithMessage("O preço deve ser maior que zero.")
            .When(p => p.Preco.HasValue);

        RuleFor(p => p.Estoque)
            .GreaterThanOrEqualTo(0).WithMessage("O estoque não pode ser negativo.")
            .When(p => p.Estoque.HasValue);
            
        RuleFor(p => p.Descricao)
            .NotEmpty().WithMessage("A descrição não pode ser vazia se enviada.")
            .When(p => p.Descricao != null);
            
        RuleFor(p => p.CategoriaId)
            .NotEmpty().WithMessage("O ID da categoria não pode ser vazio.")
            .When(p => p.CategoriaId != null);
    }
}