using NArchitecture.Core.Application.Responses;

namespace Application.Features.Addresses.Commands.Create;

public class CreatedAddressResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string AddressTitle { get; set; }
    public string FullName { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string Country { get; set; }
    public string PhoneNumber { get; set; }
}