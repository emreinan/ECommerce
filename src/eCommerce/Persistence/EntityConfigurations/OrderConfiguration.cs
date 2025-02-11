using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders").HasKey(o => o.Id);

        builder.Property(o => o.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(o => o.ShippingAddressId).HasColumnName("ShippingAddressId").IsRequired();
        builder.Property(o => o.DiscountId).HasColumnName("DiscountId");
        builder.Property(o => o.OrderCode).HasColumnName("OrderCode").IsRequired().HasMaxLength(50);
        builder.Property(o => o.OrderDate).HasColumnName("OrderDate").IsRequired();
        builder.Property(o => o.TaxAmount).HasColumnName("TaxAmount").IsRequired().HasPrecision(18, 2);
        builder.Property(o => o.ShippingCost).HasColumnName("ShippingCost").IsRequired().HasPrecision(18, 2);
        builder.Property(o => o.FinalAmount).HasColumnName("FinalAmount").IsRequired().HasPrecision(18, 2);
        builder.Property(o => o.Status).HasColumnName("Status").IsRequired();
        builder.Property(o => o.IsPaid).HasColumnName("IsPaid").IsRequired();
        builder.Property(o => o.PaymentMethod).HasColumnName("PaymentMethod").IsRequired();

        builder.Property(o => o.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(o => o.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(o => o.DeletedDate).HasColumnName("DeletedDate");

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

        builder.HasQueryFilter(o => !o.DeletedDate.HasValue);
    }
}