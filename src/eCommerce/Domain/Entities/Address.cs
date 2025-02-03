using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class Address : Entity<Guid>
{
    public Guid UserId { get; set; }
    public string AddressTitle { get; set; } // Ev, iş yeri gibi
    public string FullName { get; set; } // Teslim alacak kişinin adı
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }
    public string PhoneNumber { get; set; }

    public virtual User User { get; set; }
}
