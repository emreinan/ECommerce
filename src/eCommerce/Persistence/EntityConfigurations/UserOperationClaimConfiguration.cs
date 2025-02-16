using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class UserOperationClaimConfiguration : BaseEntityConfiguration<UserOperationClaim, Guid>
{
    public override void Configure(EntityTypeBuilder<UserOperationClaim> builder)
    {
        base.Configure(builder);
        builder.Property(uoc => uoc.UserId).IsRequired();
        builder.Property(uoc => uoc.OperationClaimId).IsRequired();

        builder.HasOne(uoc => uoc.User)
               .WithMany(u => u.UserOperationClaims)
               .HasForeignKey(uoc => uoc.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(uoc => uoc.OperationClaim)
               .WithMany()
               .HasForeignKey(uoc => uoc.OperationClaimId);

        builder.HasData(GetUserOperationClaims());
    }

    private static List<UserOperationClaim> GetUserOperationClaims()
    {
        return
        [
            new UserOperationClaim
            {
                Id = Guid.NewGuid(),
                UserId = UserConfiguration.AdminId,
                OperationClaimId = OperationClaimConfiguration.AdminId
            },
        ];
    }
}
