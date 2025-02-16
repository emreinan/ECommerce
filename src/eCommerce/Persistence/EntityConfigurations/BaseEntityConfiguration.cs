using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NArchitecture.Core.Persistence.Repositories;

namespace Persistence.EntityConfigurations;

public abstract class BaseEntityConfiguration<T, TKey> : IEntityTypeConfiguration<T> where T : Entity<TKey>
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.CreatedDate).IsRequired().ValueGeneratedOnAdd().HasDefaultValueSql("GETUTCDATE()");
        builder.Property(a => a.UpdatedDate).ValueGeneratedOnUpdate().HasDefaultValueSql("GETUTCDATE()");
        builder.Property(a => a.DeletedDate).HasDefaultValueSql("GETUTCDATE()");
        builder.HasQueryFilter(a => !a.DeletedDate.HasValue);
    }
}