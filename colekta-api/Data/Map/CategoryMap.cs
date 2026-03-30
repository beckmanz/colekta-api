using colekta_api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace colekta_api.Data.Map;

public class CategoryMap : IEntityTypeConfiguration<CategoryModel>
{
    public void Configure(EntityTypeBuilder<CategoryModel> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(category => category.Id);

        builder.Property(category => category.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(category => category.Slug)
            .HasMaxLength(120)
            .IsRequired();

        builder.HasIndex(category => category.Slug)
            .IsUnique();
    }
}

