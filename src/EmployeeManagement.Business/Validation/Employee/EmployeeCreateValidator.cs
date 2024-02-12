using EmployeeManagement.Common.DTOs.Employee;
using FluentValidation;

namespace EmployeeManagement.Business.Validation.Employee;

public class EmployeeCreateValidator : AbstractValidator<EmployeeCreate>
{
    public EmployeeCreateValidator()
    {
        RuleFor(employeeCreate => employeeCreate.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(employeeCreate => employeeCreate.LastName).NotEmpty().MaximumLength(100);
    }
}
