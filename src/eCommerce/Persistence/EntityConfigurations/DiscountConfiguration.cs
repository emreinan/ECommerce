using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class DiscountConfiguration : BaseEntityConfiguration<Discount, Guid>
{
    public override void Configure(EntityTypeBuilder<Discount> builder)
    {
        base.Configure(builder);
        builder.Property(d => d.Code).IsRequired().HasMaxLength(20);
        builder.Property(d => d.Amount).IsRequired().HasPrecision(18, 2);
        builder.Property(d => d.Percentage).HasPrecision(6, 3);
        builder.Property(d => d.MinOrderAmount).HasPrecision(18, 2);
        builder.Property(d => d.StartDate).IsRequired();
        builder.Property(d => d.EndDate).IsRequired();
        builder.Property(d => d.UsageLimit).IsRequired();
        builder.Property(d => d.IsActive).IsRequired().HasDefaultValue(true);
    }
}