using FluentValidation;

namespace colekta_api.Filters;

public class MultipartValidationFilter<T> : IEndpointFilter where T : class
{
    private readonly IValidator<T> _validator;

    public MultipartValidationFilter(IValidator<T> validator)
    {
        _validator = validator;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var request = context.HttpContext.Request;

        if (!request.HasFormContentType)
        {
            return Results.BadRequest(new { error = "Content-Type deve ser multipart/form-data." });
        }

        try
        {
            var form = await request.ReadFormAsync();
        }
        catch (Exception)
        {
            return Results.BadRequest(new { error = "Formulário inválido ou boundary ausente (Client error)." });
        }

        var dto = context.Arguments.FirstOrDefault(x => x is T) as T;
        if (dto == null) return Results.BadRequest(new { error = "Dados do produto não encontrados." });

        var argument = context.Arguments.FirstOrDefault(x => x is T) as T;
        if (argument == null) return await next(context);
        
        var validationResult = await _validator.ValidateAsync(argument);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }
        return await next(context);
    }
}