using Application.Features.Addresses.Constants;
using Application.Features.Addresses.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Addresses.Constants.AddressesOperationClaims;

namespace Application.Features.Addresses.Commands.Create;

public class CreateAddressCommand : IRequest<CreatedAddressResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public required Guid UserId { get; set; }
    public required string AddressTitle { get; set; }
    public required string FullName { get; set; }
    public required string Street { get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
    public required string ZipCode { get; set; }
    public required string Country { get; set; }
    public required string PhoneNumber { get; set; }

    public string[] Roles => [Admin, Write, AddressesOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetAddresses"];

    public class CreateAddressCommandHandler(IMapper mapper, IAddressRepository addressRepository,
                                     AddressBusinessRules addressBusinessRules) : IRequestHandler<CreateAddressCommand, CreatedAddressResponse>
    {
        public async Task<CreatedAddressResponse> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            Address address = mapper.Map<Address>(request);

            await addressRepository.AddAsync(address);

            CreatedAddressResponse response = mapper.Map<CreatedAddressResponse>(address);
            return response;
        }
    }
}