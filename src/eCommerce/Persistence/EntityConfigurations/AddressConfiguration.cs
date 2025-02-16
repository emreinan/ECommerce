using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class AddressConfiguration : BaseEntityConfiguration<Address, Guid>
{
    public override void Configure(EntityTypeBuilder<Address> builder)
    {
        base.Configure(builder);
        builder.HasIndex(a => a.AddressTitle).IsUnique();

        builder.Property(a => a.UserId).IsRequired();
        builder.Property(a => a.AddressTitle).IsRequired().HasMaxLength(50);
        builder.Property(a => a.FullName).IsRequired().HasMaxLength(100);
        builder.Property(a => a.Street).IsRequired().HasMaxLength(200);
        builder.Property(a => a.City).IsRequired().HasMaxLength(100);
        builder.Property(a => a.State).IsRequired().HasMaxLength(100);
        builder.Property(a => a.ZipCode).IsRequired().HasMaxLength(20);
        builder.Property(a => a.Country).IsRequired().HasMaxLength(100);
        builder.Property(a => a.PhoneNumber).IsRequired().HasMaxLength(20);

        builder.HasOne(a => a.User)
               .WithMany()
               .HasForeignKey(a => a.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
