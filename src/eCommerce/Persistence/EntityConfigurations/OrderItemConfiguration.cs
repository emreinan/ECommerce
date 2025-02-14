using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems").HasKey(oi => oi.Id);

        builder.Property(oi => oi.OrderId).HasColumnName("OrderId").IsRequired();
        builder.Property(oi => oi.ProductId).HasColumnName("ProductId").IsRequired();
        builder.Property(oi => oi.ProductNameAtOrderTime).HasColumnName("ProductNameAtOrderTime").IsRequired().HasMaxLength(100);
        builder.Property(oi => oi.ProductPriceAtOrderTime).HasColumnName("ProductPriceAtOrderTime").IsRequired().HasPrecision(18, 2);
        builder.Property(oi => oi.Quantity).HasColumnName("Quantity").IsRequired();
        builder.Ignore(oi => oi.TotalPrice); // Derived Property, veritabanưnda tutulmuyor.

       // builder.Property(oi => oi.TotalPrice)
       //.HasComputedColumnSql("[ProductPriceAtOrderTime] * [Quantity]");

        builder.Property(oi => oi.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(oi => oi.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(oi => oi.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(oi => oi.Product)
            .WithMany()
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasQueryFilter(oi => !oi.DeletedDate.HasValue);
    }
}