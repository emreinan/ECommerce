using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class OrderHistoryConfiguration : IEntityTypeConfiguration<OrderHistory>
{
    public void Configure(EntityTypeBuilder<OrderHistory> builder)
    {
        builder.ToTable("OrderHistories").HasKey(oh => oh.Id);

        builder.Property(oh => oh.OrderId).HasColumnName("OrderId").IsRequired();
        builder.Property(oh => oh.Status).HasColumnName("Status").IsRequired();
        builder.Property(oh => oh.ChangedAt).HasColumnName("ChangedAt").IsRequired();
        builder.Property(oh => oh.ChangedBy).HasColumnName("ChangedBy").IsRequired().HasMaxLength(50);

        builder.Property(oh => oh.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(oh => oh.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(oh => oh.DeletedDate).HasColumnName("DeletedDate");

        builder.HasOne(oh => oh.Order)
            .WithMany()
            .HasForeignKey(oh => oh.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(oh => !oh.DeletedDate.HasValue);
    }
}