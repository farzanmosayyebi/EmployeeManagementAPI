using EmployeeManagement.Common.DTOs.Address;
using FluentValidation;

namespace EmployeeManagement.Business.Validation.Address;

public class AddressUpdateValidator : AbstractValidator<AddressUpdate>
{
    public AddressUpdateValidator()
    {
        RuleFor(addressUpdate => addressUpdate.City).NotEmpty().MaximumLength(100);
        RuleFor(addressUpdate => addressUpdate.Street).NotEmpty().MaximumLength(100);
        RuleFor(addressUpdate => addressUpdate.Zip).NotEmpty().MaximumLength(10);
        RuleFor(addressUpdate => addressUpdate.Phone).NotEmpty().MaximumLength(11);
    }
}
