using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NArchitecture.Core.Security.Entities;
using NArchitecture.Core.Security.Enums;
using NArchitecture.Core.Security.Hashing;

namespace Persistence.EntityConfigurations;

public class UserConfiguration : BaseEntityConfiguration<User, Guid>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
        builder.Property(u => u.PasswordSalt).IsRequired().HasMaxLength(256);
        builder.Property(u => u.PasswordHash).IsRequired().HasMaxLength(256);
        builder.Property(u => u.AuthenticatorType).IsRequired();

        builder.HasMany(u => u.UserOperationClaims)
               .WithOne(uoc => uoc.User)
               .HasForeignKey(uoc => uoc.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.RefreshTokens)
               .WithOne(rt => rt.User)
               .HasForeignKey(rt => rt.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.EmailAuthenticators)
               .WithOne(ea => ea.User)
               .HasForeignKey(ea => ea.UserId);

        builder.HasMany(u => u.OtpAuthenticators)
               .WithOne(oa => oa.User)
               .HasForeignKey(oa => oa.UserId);

        builder.HasData(GetUsers());
    }

    public static Guid AdminId { get; } = Guid.NewGuid();

    private static List<User> GetUsers()
    {
        HashingHelper.CreatePasswordHash("1234", out byte[] PasswordHash, out byte[] PasswordSalt);

        return
        [
            new User
            {
                Id = AdminId,
                FirstName = "Admin",
                LastName = "admin",
                PhoneNumber = "1234567890",
                DateOfBirth = DateTime.UtcNow,
                Email = "admin@mail.com",
                PasswordHash = PasswordHash,
                PasswordSalt = PasswordSalt,
            }
        ];
    }

}
