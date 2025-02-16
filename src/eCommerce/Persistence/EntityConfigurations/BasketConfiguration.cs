using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class BasketConfiguration : BaseEntityConfiguration<Basket, Guid>
{
    public override void Configure(EntityTypeBuilder<Basket> builder)
    {
        base.Configure(builder);
        builder.Property(b => b.UserId).IsRequired();

        builder.HasOne(b => b.User)
            .WithMany()
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(b => b.BasketItems)
            .WithOne(bi => bi.Basket)
            .HasForeignKey(bi => bi.BasketId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}