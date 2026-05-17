using colekta_api.Models.RequestDtos;
using FluentValidation;

namespace colekta_api.Validators;

public class CompleteProfileDtoValidator : AbstractValidator<CompleteProfileDto>
{
    public CompleteProfileDtoValidator()
    {

        RuleFor(x => x.Cpf)
            .NotEmpty().WithMessage("O CPF é obrigatório.")
            .Matches(@"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$")
            .WithMessage("CPF inválido.");

        RuleFor(x => x.Telefone)
            .NotEmpty().WithMessage("O telefone é obrigatório.")
            .Matches(@"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$")
            .WithMessage("Telefone inválido.");
    }
}