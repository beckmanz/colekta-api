using colekta_api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace colekta_api.Data.Map;

public class CartMap : IEntityTypeConfiguration<CartModel>
{
    public void Configure(EntityTypeBuilder<CartModel> builder)
    {
        builder.ToTable("Carts");

        builder.HasKey(cart => cart.Id);

        builder.Property(cart => cart.UserId)
            .IsRequired();

        builder.HasIndex(cart => cart.UserId)
            .IsUnique();

        builder.HasOne(cart => cart.User)
            .WithOne(user => user.ShoppingCart)
            .HasForeignKey<CartModel>(cart => cart.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(cart => cart.Items)
            .WithOne(item => item.Cart)
            .HasForeignKey(item => item.CartId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

