using EmployeeManagement.Common.DTOs.Job;
using FluentValidation;

namespace EmployeeManagement.Business.Validation.Job;

public class JobCreateValidator : AbstractValidator<JobCreate>
{
    public JobCreateValidator()
    {
        RuleFor(jobCreate => jobCreate.Name).NotEmpty().MaximumLength(100);
        RuleFor(jobCreate => jobCreate.Description).MaximumLength(250);
    }
}
