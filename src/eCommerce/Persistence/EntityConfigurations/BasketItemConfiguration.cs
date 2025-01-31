using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
{
    public void Configure(EntityTypeBuilder<BasketItem> builder)
    {
        builder.ToTable("BasketItems").HasKey(bi => bi.Id);

        builder.Property(bi => bi.Id).HasColumnName("Id").IsRequired();
        builder.Property(bi => bi.BasketId).HasColumnName("BasketId").IsRequired();
        builder.Property(bi => bi.ProductId).HasColumnName("ProductId").IsRequired();
        builder.Property(bi => bi.Quantity).HasColumnName("Quantity").IsRequired();
        builder.Property(bi => bi.Price).HasColumnName("Price").IsRequired();
        builder.Property(bi => bi.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(bi => bi.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(bi => bi.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(bi => !bi.DeletedDate.HasValue);
    }
}