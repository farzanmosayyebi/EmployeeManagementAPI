using EmployeeManagement.Common.DTOs.Address;
using FluentValidation;

namespace EmployeeManagement.Business.Validation.Address;

public class AddressCreateValidator : AbstractValidator<AddressCreate>
{
    public AddressCreateValidator()
    {
        RuleFor(addressCreate => addressCreate.City).NotEmpty().MaximumLength(100);
        RuleFor(addressCreate => addressCreate.Street).NotEmpty().MaximumLength(100);
        RuleFor(addressCreate => addressCreate.Zip).NotEmpty().MaximumLength(10);
        RuleFor(addressCreate => addressCreate.Phone).NotEmpty().MaximumLength(11);
    }
}
