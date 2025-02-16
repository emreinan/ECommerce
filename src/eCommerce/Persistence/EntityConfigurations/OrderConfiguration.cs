using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class OrderConfiguration : BaseEntityConfiguration<Order, Guid>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        base.Configure(builder);
        builder.HasIndex(o => o.OrderCode).IsUnique();

        builder.Property(o => o.UserId).IsRequired();
        builder.Property(o => o.ShippingAddressId).IsRequired();
        builder.Property(o => o.DiscountId).IsRequired(false);
        builder.Property(o => o.OrderCode).IsRequired().HasMaxLength(50);
        builder.Property(o => o.OrderDate).IsRequired();
        builder.Property(o => o.TaxAmount).IsRequired().HasPrecision(18, 2);
        builder.Property(o => o.ShippingCost).IsRequired().HasPrecision(18, 2);
        builder.Property(o => o.FinalAmount).IsRequired().HasPrecision(18, 2);
        builder.Property(o => o.IsPaid).IsRequired();
        builder.Property(o => o.Status).IsRequired();
        builder.Property(o => o.PaymentMethod).IsRequired();

        builder.HasOne(o => o.User)
            .WithMany()
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.ShippingAddress)
            .WithMany()
            .HasForeignKey(o => o.ShippingAddressId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId);
    }
}