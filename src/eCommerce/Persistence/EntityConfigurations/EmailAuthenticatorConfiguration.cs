using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class EmailAuthenticatorConfiguration : BaseEntityConfiguration<EmailAuthenticator, Guid>
{
    public override void Configure(EntityTypeBuilder<EmailAuthenticator> builder)
    {
        base.Configure(builder);
        builder.Property(ea => ea.UserId).IsRequired();
        builder.Property(ea => ea.ActivationKey).IsRequired(false);
        builder.Property(ea => ea.IsVerified).IsRequired();

        builder.HasOne(ea => ea.User);

        builder.HasBaseType((string)null!);
    }
}
