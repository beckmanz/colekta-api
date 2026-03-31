using colekta_api.Data;
using colekta_api.Endpoints;
using colekta_api.Extensions;
using colekta_api.Middlewares;
using colekta_api.Models.Entities;
using colekta_api.Services.Authentication;
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
builder.Services.AddAuthorization();
builder.Services.AddColektaCors(builder.Configuration);
builder.Services.AddColektaDocumentation();

var app = builder.Build();

await DbInitializer.SeedRolesAsync(app.Services);
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseColektaDocumentation();
app.UseColektaCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapAuthenticationEndpoints();
app.UseHttpsRedirection();
app.Run();