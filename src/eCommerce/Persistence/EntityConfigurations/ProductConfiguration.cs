using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ProductConfiguration : BaseEntityConfiguration<Product, Guid>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);
        builder.Property(p => p.SellerId).IsRequired();
        builder.Property(p => p.CategoryId).IsRequired();
        builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
        builder.Property(p => p.Price).IsRequired().HasPrecision(18, 2);
        builder.Property(p => p.Details).HasMaxLength(500);
        builder.Property(p => p.StockAmount).IsRequired();
        builder.Property(p => p.Enabled).IsRequired().HasDefaultValue(true);

        builder.ToTable(tb => tb.HasCheckConstraint("CHK_Product_StockAmount", "[StockAmount] >= 0"));
        builder.ToTable(tb => tb.HasCheckConstraint("CHK_Product_Price", "[Price] > 0"));

        builder.HasOne(p => p.Category)
         .WithMany(c => c.Products)
         .HasForeignKey(p => p.CategoryId);

        builder.HasOne(p => p.User)
           .WithMany()
           .HasForeignKey(p => p.SellerId)
           .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.ProductImages)
            .WithOne(pi => pi.Product)
            .HasForeignKey(pi => pi.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.ProductComments)
            .WithOne(pc => pc.Product)
            .HasForeignKey(pc => pc.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.BasketItems)
            .WithOne(bi => bi.Product)
            .HasForeignKey(bi => bi.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.OrderItems)
            .WithOne(oi => oi.Product)
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}