using EmployeeManagement.Common.DTOs.Team;
using FluentValidation;

namespace EmployeeManagement.Business.Validation.Team;

public class TeamCreateValidator : AbstractValidator<TeamCreate>
{
    public TeamCreateValidator()
    {
        RuleFor(teamCreate => teamCreate.Name).NotEmpty().MaximumLength(50);
    }
}
