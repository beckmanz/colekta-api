using Microsoft.OpenApi;
using Scalar.AspNetCore;

namespace colekta_api.Extensions;

public static class ScalarExtensions
{
    public static IServiceCollection AddColektaDocumentation(this IServiceCollection services)
    {
        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, _, _) =>
            {
                document.Info.Title = "Colekta API";
                document.Info.Version = "v1";
                document.Info.Description = "Plataforma de e-commerce para colecionáveis e raridades.";

                document.Components ??= new OpenApiComponents();

                document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();

                var securityScheme = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Cookie,
                    Name = "ColektaAccessToken",
                    Description = "Autenticação via Cookie HttpOnly"
                };

                document.Components.SecuritySchemes.TryAdd("CookieAuth", securityScheme);
                
                document.Tags = new HashSet<OpenApiTag>
                {
                    new OpenApiTag 
                    { 
                        Name = "Authentication", 
                        Description = "Endpoints relacionados à autenticação de usuários, incluindo login, registro e logout." 
                    }
                };

                return Task.CompletedTask;
            });
        });

        return services;
    }

    public static IApplicationBuilder UseColektaDocumentation(this IApplicationBuilder app)
    {
        app.UseRouting();
        
        if (app is IEndpointRouteBuilder endpointApp)
        {
            endpointApp.MapOpenApi();
            endpointApp.MapScalarApiReference(options =>
            {
                options
                    .WithTitle("Colekta API - Docs")
                    .WithTheme(ScalarTheme.Default)
                    .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
            });
        }

        return app;
    }
}