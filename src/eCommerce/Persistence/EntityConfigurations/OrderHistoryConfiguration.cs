using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class OrderHistoryConfiguration : BaseEntityConfiguration<OrderHistory, Guid>
{
    public override void Configure(EntityTypeBuilder<OrderHistory> builder)
    {
        base.Configure(builder);
        builder.Property(oh => oh.OrderId).IsRequired();
        builder.Property(oh => oh.Status).IsRequired();
        builder.Property(oh => oh.ChangedAt).IsRequired();
        builder.Property(oh => oh.ChangedBy).IsRequired().HasMaxLength(50);

        builder.HasOne(oh => oh.Order)
            .WithMany()
            .HasForeignKey(oh => oh.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}