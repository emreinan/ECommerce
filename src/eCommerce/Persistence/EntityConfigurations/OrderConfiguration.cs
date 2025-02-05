using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders").HasKey(o => o.Id);

        builder.Property(o => o.Id).HasColumnName("Id").IsRequired();
        builder.Property(o => o.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(o => o.ShippingAddressId).HasColumnName("ShippingAddressId").IsRequired();
        builder.Property(o => o.DiscountId).HasColumnName("DiscountId");
        builder.Property(o => o.OrderCode).HasColumnName("OrderCode").IsRequired();
        builder.Property(o => o.OrderDate).HasColumnName("OrderDate").IsRequired();
        builder.Property(o => o.TaxAmount).HasColumnName("TaxAmount").IsRequired();
        builder.Property(o => o.ShippingCost).HasColumnName("ShippingCost").IsRequired();
        builder.Property(o => o.FinalAmount).HasColumnName("FinalAmount").IsRequired();
        builder.Property(o => o.Status).HasColumnName("Status").IsRequired();
        builder.Property(o => o.IsPaid).HasColumnName("IsPaid").IsRequired();
        builder.Property(o => o.PaymentMethod).HasColumnName("PaymentMethod").IsRequired();
        builder.Property(o => o.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(o => o.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(o => o.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(o => !o.DeletedDate.HasValue);
    }
}