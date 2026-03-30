using colekta_api.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace colekta_api.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUserModel>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<AddressModel> Addresses { get; set; } = null!;
    public DbSet<CategoryModel> Categories { get; set; } = null!;
    public DbSet<ProductModel> Products { get; set; } = null!;
    public DbSet<ProductImageModel> ProductImages { get; set; } = null!;
    public DbSet<CartModel> Carts { get; set; } = null!;
    public DbSet<CartItemModel> CartItems { get; set; } = null!;
    public DbSet<OrderModel> Orders { get; set; } = null!;
    public DbSet<OrderItemModel> OrderItems { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
