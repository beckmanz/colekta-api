using System.Net;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace colekta_api.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IWebHostEnvironment _environment;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger,
        IWebHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, title, detail) = exception switch
        {
            ArgumentNullException => (HttpStatusCode.BadRequest, "Requisicao invalida", exception.Message),
            ArgumentException => (HttpStatusCode.BadRequest, "Requisicao invalida", exception.Message),
            KeyNotFoundException => (HttpStatusCode.NotFound, "Recurso nao encontrado", exception.Message),
            UnauthorizedAccessException => (HttpStatusCode.Unauthorized, "Acesso nao autorizado", "Voce nao tem permissao para acessar este recurso."),
            InvalidOperationException => (HttpStatusCode.UnprocessableEntity, "Entidade nao processavel", exception.Message),
            _ => (HttpStatusCode.InternalServerError, "Erro interno do servidor", "Ocorreu um erro interno. Tente novamente mais tarde.")
        };

        if (!_environment.IsDevelopment())
        {
            detail = statusCode switch
            {
                HttpStatusCode.BadRequest => "Verifique os dados enviados e tente novamente.",
                HttpStatusCode.NotFound => "O recurso solicitado nao foi encontrado.",
                HttpStatusCode.Unauthorized => "Voce nao tem permissao para acessar este recurso.",
                HttpStatusCode.UnprocessableEntity => "Nao foi possivel processar a requisicao.",
                _ => "Ocorreu um erro interno. Tente novamente mais tarde."
            };
        }

        var problemDetails = new ProblemDetails
        {
            Type = $"https://httpstatuses.io/{(int)statusCode}",
            Title = title,
            Status = (int)statusCode,
            Detail = detail,
            Instance = context.Request.Path
        };

        problemDetails.Extensions["traceId"] = Activity.Current?.Id ?? context.TraceIdentifier;

        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = (int)statusCode;

        var json = JsonSerializer.Serialize(problemDetails, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        return context.Response.WriteAsync(json);
    }
}


