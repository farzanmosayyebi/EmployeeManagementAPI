using EmployeeManagement.Common.DTOs.Employee;
using FluentValidation;

namespace EmployeeManagement.Business.Validation.Employee;

public class EmployeeUpdateValidator : AbstractValidator<EmployeeUpdate>
{
    public EmployeeUpdateValidator()
    {
        RuleFor(employeeUpdate => employeeUpdate.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(employeeUpdate => employeeUpdate.LastName).NotEmpty().MaximumLength(100);
    }
}
