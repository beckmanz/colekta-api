namespace colekta_api.Extensions;

public static class CorsExtensions
{
    private const string PolicyName = "ColektaCorsPolicy";

    public static IServiceCollection AddColektaCors(this IServiceCollection services, IConfiguration configuration)
    {
        var allowedOrigins = configuration
            .GetSection("Cors:AllowedOrigins")
            .Get<string[]>()
            ?? configuration["Cors:AllowedOrigins"]?
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            ?? [];

        if (allowedOrigins.Length == 0)
        {
            throw new InvalidOperationException(
                "Nenhuma origem foi configurada para o CORS. Configure 'Cors:AllowedOrigins'.");
        }

        services.AddCors(options =>
        {
            options.AddPolicy(PolicyName, policy =>
            {
                policy
                    .WithOrigins(allowedOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        return services;
    }

    public static IApplicationBuilder UseColektaCors(this IApplicationBuilder app)
    {
        app.UseCors(PolicyName);
        return app;
    }
}

