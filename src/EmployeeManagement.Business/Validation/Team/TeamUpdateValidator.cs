using EmployeeManagement.Common.DTOs.Team;
using FluentValidation;

namespace EmployeeManagement.Business.Validation.Team;

public class TeamUpdateValidator : AbstractValidator<TeamUpdate>
{
    public TeamUpdateValidator()
    {
        RuleFor(teamUpdate => teamUpdate.Name).NotEmpty().MaximumLength(50);
    }
}
