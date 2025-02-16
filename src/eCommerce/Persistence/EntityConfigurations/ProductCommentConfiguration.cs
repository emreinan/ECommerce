using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ProductCommentConfiguration : BaseEntityConfiguration<ProductComment, Guid>
{
    public override void Configure(EntityTypeBuilder<ProductComment> builder)
    {
        base.Configure(builder);
        builder.ToTable(tb => tb.HasCheckConstraint("CHK_ProductComment_StarCount", "[StarCount] BETWEEN 1 AND 5"));

        builder.Property(pc => pc.ProductId).IsRequired();
        builder.Property(pc => pc.UserId).IsRequired();
        builder.Property(pc => pc.Text).IsRequired().HasMaxLength(500);
        builder.Property(pc => pc.StarCount).IsRequired();
        builder.Property(pc => pc.IsConfirmed).IsRequired().HasDefaultValue(false);

        builder.HasOne(pc => pc.Product)
            .WithMany(p => p.ProductComments)
            .HasForeignKey(pc => pc.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(pc => pc.User)
            .WithMany()
            .HasForeignKey(pc => pc.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}