using EmployeeManagement.Common.DTOs.Employee;
using FluentValidation;

namespace EmployeeManagement.Business.Validation.Employee;

public class EmployeeFilterValidator : AbstractValidator<EmployeeFilter>
{
    public EmployeeFilterValidator()
    {
        RuleFor(employeeFilter => employeeFilter.FirstName).MaximumLength(50);
        RuleFor(employeeFilter => employeeFilter.LastName).MaximumLength(100);
    }
}
