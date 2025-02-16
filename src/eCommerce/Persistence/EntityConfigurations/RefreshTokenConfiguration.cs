using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class RefreshTokenConfiguration : BaseEntityConfiguration<RefreshToken, Guid>
{
    public override void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        base.Configure(builder);
        builder.Property(rt => rt.UserId).IsRequired();
        builder.Property(rt => rt.Token).IsRequired();
        builder.Property(rt => rt.ExpirationDate).IsRequired().HasMaxLength(512);
        builder.Property(rt => rt.CreatedByIp).IsRequired().HasMaxLength(45);
        builder.Property(rt => rt.RevokedDate);
        builder.Property(rt => rt.RevokedByIp).HasMaxLength(45);
        builder.Property(rt => rt.ReplacedByToken).HasMaxLength(512);
        builder.Property(rt => rt.ReasonRevoked).HasMaxLength(250);

        builder.HasOne(rt => rt.User)
               .WithMany() 
               .HasForeignKey(rt => rt.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
