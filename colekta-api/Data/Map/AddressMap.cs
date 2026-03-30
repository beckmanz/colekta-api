using colekta_api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace colekta_api.Data.Map;

public class AddressMap : IEntityTypeConfiguration<AddressModel>
{
    public void Configure(EntityTypeBuilder<AddressModel> builder)
    {
        builder.ToTable("Addresses");

        builder.HasKey(address => address.Id);

        builder.Property(address => address.Street)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(address => address.Number)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(address => address.Complement)
            .HasMaxLength(100);

        builder.Property(address => address.Neighborhood)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(address => address.City)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(address => address.State)
            .HasMaxLength(2)
            .IsRequired();

        builder.Property(address => address.ZipCode)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(address => address.UserId)
            .IsRequired();

        builder.HasOne(address => address.User)
            .WithMany(user => user.Addresses)
            .HasForeignKey(address => address.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

