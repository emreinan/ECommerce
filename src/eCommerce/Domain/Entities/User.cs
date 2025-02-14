namespace Domain.Entities;

public class User : NArchitecture.Core.Security.Entities.User<Guid>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public DateTime DateOfBirth { get; set; }
    public string? ProfileImageUrl { get; set; }
    public bool IsActive { get; set; } = true;


    public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; } = default!;
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = default!;
    public virtual ICollection<OtpAuthenticator> OtpAuthenticators { get; set; } = default!;
    public virtual ICollection<EmailAuthenticator> EmailAuthenticators { get; set; } = default!;

    public virtual ICollection<Address> Addresses { get; set; } = default!;
    public virtual ICollection<Order> Orders { get; set; } = default!;
    public virtual ICollection<Product> Products { get; set; } = default!;
    public virtual ICollection<ProductComment> ProductComments { get; set; } = default!;
    public virtual Basket? Basket { get; set; }

}
