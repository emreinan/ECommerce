using FluentValidation;

namespace Application.Features.Addresses.Commands.Create;

public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
{
    public CreateAddressCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.AddressTitle).NotEmpty();
        RuleFor(c => c.FullName).NotEmpty();
        RuleFor(c => c.Street).NotEmpty();
        RuleFor(c => c.City).NotEmpty();
        RuleFor(c => c.State).NotEmpty();
        RuleFor(c => c.ZipCode).NotEmpty();
        RuleFor(c => c.Country).NotEmpty();
        RuleFor(c => c.PhoneNumber).NotEmpty();
    }
}