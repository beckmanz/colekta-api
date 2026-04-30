using colekta_api.Data;
using colekta_api.Endpoints;
using colekta_api.Extensions;
using colekta_api.Middlewares;
using colekta_api.Models.Entities;
using colekta_api.Repositories;
using colekta_api.Repositories.Interfaces;
using colekta_api.Services.Authentication;
using colekta_api.Services.File;
using colekta_api.Services.Product;
using colekta_api.Services.Token;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentityCore<ApplicationUserModel>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddScoped<IAuthenticationInterface, AuthenticationService>();
builder.Services.AddScoped<ITokenInterface, TokenService>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ElevatedRights", policy => policy.RequireRole("Admin", "Creator"));
    options.AddPolicy("Common", policy => policy.RequireRole("Vendedor", "Admin", "Creator"));
});
builder.Services.AddColektaCors(builder.Configuration);
builder.Services.AddColektaDocumentation(builder.Configuration);
builder.Services.AddScoped<IProductInterface, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IFileInterface, FileService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}
await DbInitializer.SeedRolesAsync(app.Services);
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseColektaDocumentation();
app.UseColektaCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapAuthenticationEndpoints();
app.MapProductEndpoints();
app.UseHttpsRedirection();
app.Run();