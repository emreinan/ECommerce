using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.ToTable("Discounts").HasKey(d => d.Id);

        builder.Property(d => d.Id).HasColumnName("Id").IsRequired();
        builder.Property(d => d.Code).HasColumnName("Code").IsRequired();
        builder.Property(d => d.Amount).HasColumnName("Amount").IsRequired();
        builder.Property(d => d.Percentage).HasColumnName("Percentage");
        builder.Property(d => d.MinOrderAmount).HasColumnName("MinOrderAmount");
        builder.Property(d => d.StartDate).HasColumnName("StartDate").IsRequired();
        builder.Property(d => d.EndDate).HasColumnName("EndDate").IsRequired();
        builder.Property(d => d.IsActive).HasColumnName("IsActive").IsRequired();
        builder.Property(d => d.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(d => d.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(d => d.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(d => !d.DeletedDate.HasValue);
    }
}