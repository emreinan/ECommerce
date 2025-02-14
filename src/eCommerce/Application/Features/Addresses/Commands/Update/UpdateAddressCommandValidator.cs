using FluentValidation;

namespace Application.Features.Addresses.Commands.Update;

public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
{
    public UpdateAddressCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.AddressTitle).NotEmpty().MaximumLength(50);
        RuleFor(c => c.FullName).NotEmpty().MaximumLength(50);
        RuleFor(c => c.Street).NotEmpty().MaximumLength(200);
        RuleFor(c => c.City).NotEmpty().MaximumLength(100);
        RuleFor(c => c.State).NotEmpty().MaximumLength(100);
        RuleFor(c => c.ZipCode).NotEmpty().MaximumLength(20);
        RuleFor(c => c.Country).NotEmpty().MaximumLength(100);
        RuleFor(c => c.PhoneNumber).NotEmpty().MaximumLength(20);
    }
}