using Microsoft.AspNetCore.Identity;

namespace colekta_api.Models.ResultsModel;

public static class ResultExtensions
{
    // ── Sucesso ────────────────────────────────────────────────────────────

    public static IResult ToOkResult<T>(this T data, string message = "")
        => TypedResults.Ok(ResultData<T>.Success(data, message));

    public static IResult ToCreatedResult<T>(this T data, string location, string message = "")
        => TypedResults.Created(location, ResultData<T>.Success(data, message));

    public static IResult ToAcceptedResult<T>(this T data, string? location = null, string message = "")
        => TypedResults.Accepted(location, ResultData<T>.Success(data, message));

    public static IResult ToNoContentResult(this Result _)
        => TypedResults.NoContent();

    // ── Erro — mensagem única ──────────────────────────────────────────────

    public static IResult ToBadRequestResult(this string message)
        => TypedResults.BadRequest(ResultData<object?>.Error(message));

    public static IResult ToNotFoundResult(this string message)
        => TypedResults.NotFound(ResultData<object?>.Error(message));

    public static IResult ToConflictResult(this string message)
        => TypedResults.Conflict(ResultData<object?>.Error(message));

    public static IResult ToUnprocessableEntityResult(this string message)
        => TypedResults.UnprocessableEntity(ResultData<object?>.Error(message));

    public static IResult ToUnauthorizedResult(this string _)
        => TypedResults.Unauthorized();

    public static IResult ToForbiddenResult(this string _)
        => TypedResults.Forbid();

    // ── Erro — lista de erros ──────────────────────────────────────────────

    public static IResult ToBadRequestResult(this IEnumerable<string> errors)
        => TypedResults.BadRequest(ResultData<object?>.Error(errors));

    public static IResult ToNotFoundResult(this IEnumerable<string> errors)
        => TypedResults.NotFound(ResultData<object?>.Error(errors));

    public static IResult ToConflictResult(this IEnumerable<string> errors)
        => TypedResults.Conflict(ResultData<object?>.Error(errors));

    public static IResult ToUnprocessableEntityResult(this IEnumerable<string> errors)
        => TypedResults.UnprocessableEntity(ResultData<object?>.Error(errors));

    // ── Erro — IdentityResult ─────────────────────────────────────────────

    public static IResult ToBadRequestResult(this IdentityResult result)
        => result.Errors.Select(e => e.Description).ToBadRequestResult();

    public static IResult ToConflictResult(this IdentityResult result)
        => result.Errors.Select(e => e.Description).ToConflictResult();

    public static IResult ToUnprocessableEntityResult(this IdentityResult result)
        => result.Errors.Select(e => e.Description).ToUnprocessableEntityResult();

    // ── Mapper genérico ResultData<T> → IResult ───────────────────────────

    public static IResult ToHttpResult<T>(
        this ResultData<T> result,
        Func<ResultData<T>, IResult> onSuccess,
        Func<ResultData<T>, IResult>? onFailure = null)
    {
        if (result.IsSuccess)
            return onSuccess(result);

        return onFailure is not null
            ? onFailure(result)
            : TypedResults.BadRequest(result);
    }

    // ── Conveniência — T com mensagem de erro ─────────────────────────────

    public static IResult ToConflictResult<T>(this T data, string message)
        => TypedResults.Conflict(ResultData<T>.Error(message));
}