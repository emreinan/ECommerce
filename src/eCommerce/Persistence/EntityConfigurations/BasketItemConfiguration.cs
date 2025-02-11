using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
{
    public void Configure(EntityTypeBuilder<BasketItem> builder)
    {
        builder.ToTable("BasketItems").HasKey(bi => bi.Id);

        builder.Property(bi => bi.BasketId).HasColumnName("BasketId").IsRequired();
        builder.Property(bi => bi.ProductId).HasColumnName("ProductId").IsRequired();
        builder.Property(bi => bi.Quantity).IsRequired().HasDefaultValue(1);

        builder.ToTable(tb => tb.HasCheckConstraint("CHK_BasketItem_Quantity", "[Quantity] >= 1"));

        builder.Property(bi => bi.Price).HasColumnName("Price").IsRequired().HasPrecision(18, 2);

        builder.Property(bi => bi.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(bi => bi.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(bi => bi.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(bi => bi.Basket)
               .WithMany(b => b.BasketItems)
               .HasForeignKey(bi => bi.BasketId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(bi => bi.Product)
               .WithMany(p => p.BasketItems)
               .HasForeignKey(bi => bi.ProductId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(bi => !bi.DeletedDate.HasValue);
    }
}
