using colekta_api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace colekta_api.Data.Map;

public class ApplicationUserMap : IEntityTypeConfiguration<ApplicationUserModel>
{
    public void Configure(EntityTypeBuilder<ApplicationUserModel> builder)
    {
        builder.Property(user => user.FullName)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(user => user.Cpf)
            .HasMaxLength(14)
            .IsRequired(false);

        builder.HasIndex(user => user.Cpf)
            .IsUnique();
    }
}

