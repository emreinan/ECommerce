using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Addresses.Queries.GetList;

public class GetListAddressListItemDto : IDto
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