namespace EmployeeManagement.Common.DTOs.Team;

public record TeamFilter(int? Id, string? Name, int? EmployeeId, int? Skip, int? Take);
