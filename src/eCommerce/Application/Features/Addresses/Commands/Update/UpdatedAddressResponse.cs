using NArchitecture.Core.Application.Responses;

namespace Application.Features.Addresses.Commands.Update;

public class UpdatedAddressResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string AddressTitle { get; set; } = default!;    
    public string FullName { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string City { get; set; } = default!;
    public string State { get; set; } = default!;
    public string ZipCode { get; set; } = default!;
    public string Country { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
}