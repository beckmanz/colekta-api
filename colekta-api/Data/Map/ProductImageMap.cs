using colekta_api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace colekta_api.Data.Map;

public class ProductImageMap : IEntityTypeConfiguration<ProductImageModel>
{
    public void Configure(EntityTypeBuilder<ProductImageModel> builder)
    {
        builder.ToTable("ProductImages");

        builder.HasKey(image => image.Id);

        builder.Property(image => image.Url)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(image => image.Order)
            .IsRequired();

        builder.Property(image => image.IsCover)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasOne(image => image.Product)
            .WithMany(product => product.Images)
            .HasForeignKey(image => image.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}


