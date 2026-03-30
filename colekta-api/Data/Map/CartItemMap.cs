using colekta_api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace colekta_api.Data.Map;

public class CartItemMap : IEntityTypeConfiguration<CartItemModel>
{
    public void Configure(EntityTypeBuilder<CartItemModel> builder)
    {
        builder.ToTable("CartItems");

        builder.HasKey(item => item.Id);

        builder.Property(item => item.Quantity)
            .IsRequired();

        builder.HasIndex(item => new { item.CartId, item.ProductId })
            .IsUnique();

        builder.HasOne(item => item.Cart)
            .WithMany(cart => cart.Items)
            .HasForeignKey(item => item.CartId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(item => item.Product)
            .WithMany()
            .HasForeignKey(item => item.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

