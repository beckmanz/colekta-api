using Microsoft.AspNetCore.Identity;

namespace colekta_api.Data;
public static class DbInitializer
{
    public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContext>>();

        try
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles = { "Admin", "Vendedor", "Cliente" };

            foreach (var roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            logger.LogInformation("Roles inicializadas com sucesso.");
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Não foi possível inicializar as roles. A aplicação continuará sem o seed inicial.");
        }
    }
}