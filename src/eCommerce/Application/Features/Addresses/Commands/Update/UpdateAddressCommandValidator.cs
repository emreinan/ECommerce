using FluentValidation;

namespace Application.Features.Addresses.Commands.Update;

public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
{
    public UpdateAddressCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
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