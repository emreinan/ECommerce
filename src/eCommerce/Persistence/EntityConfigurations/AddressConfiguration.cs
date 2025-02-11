using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("Addresses").HasKey(a => a.Id);

        builder.Property(a => a.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(a => a.AddressTitle).HasColumnName("AddressTitle").IsRequired().HasMaxLength(50);
        builder.Property(a => a.FullName).HasColumnName("FullName").IsRequired().HasMaxLength(100);
        builder.Property(a => a.Street).HasColumnName("Street").IsRequired().HasMaxLength(200);
        builder.Property(a => a.City).HasColumnName("City").IsRequired().HasMaxLength(100);
        builder.Property(a => a.State).HasColumnName("State").IsRequired().HasMaxLength(100);
        builder.Property(a => a.ZipCode).HasColumnName("ZipCode").IsRequired().HasMaxLength(20);
        builder.Property(a => a.Country).HasColumnName("Country").IsRequired().HasMaxLength(100);
        builder.Property(a => a.PhoneNumber).HasColumnName("PhoneNumber").IsRequired().HasMaxLength(20);

        builder.Property(a => a.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(a => a.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(a => a.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(a => a.User)
               .WithMany()
               .HasForeignKey(a => a.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(a => !a.DeletedDate.HasValue);
    }
}