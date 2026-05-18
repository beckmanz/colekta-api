using colekta_api.Models.RequestDtos;
using FluentValidation;

namespace colekta_api.Validators;

public class AddCartItemValidator : AbstractValidator<AddCartItemDto>
{
    public AddCartItemValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("O ID do produto é obrigatório.");

        RuleFor(x => x.Quantidade)
            .GreaterThan(0).WithMessage("A quantidade inserida deve ser maior que zero.");
    }
}