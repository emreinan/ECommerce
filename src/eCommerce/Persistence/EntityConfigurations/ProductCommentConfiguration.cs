using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ProductCommentConfiguration : IEntityTypeConfiguration<ProductComment>
{
    public void Configure(EntityTypeBuilder<ProductComment> builder)
    {
        builder.ToTable("ProductComments").HasKey(pc => pc.Id);

        builder.Property(pc => pc.Id).HasColumnName("Id").IsRequired();
        builder.Property(pc => pc.ProductId).HasColumnName("ProductId").IsRequired();
        builder.Property(pc => pc.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(pc => pc.Text).HasColumnName("Text").IsRequired();
        builder.Property(pc => pc.StarCount).HasColumnName("StarCount").IsRequired();
        builder.Property(pc => pc.IsConfirmed).HasColumnName("IsConfirmed").IsRequired();
        builder.Property(pc => pc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(pc => pc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(pc => pc.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(pc => !pc.DeletedDate.HasValue);
    }
}