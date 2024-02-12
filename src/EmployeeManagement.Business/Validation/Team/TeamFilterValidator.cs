using EmployeeManagement.Common.DTOs.Team;
using FluentValidation;

namespace EmployeeManagement.Business.Validation.Team;

public class TeamFilterValidator : AbstractValidator<TeamFilter>
{
    public TeamFilterValidator()
    {
        RuleFor(teamFilter => teamFilter.Name).MaximumLength(50);
    }
}
