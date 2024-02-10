namespace EmployeeManagement.Common.DTOs.Employee;

public record EmployeeFilter(string? FirstName, string? LastName, int? AddressId, int? JobId, int? TeamId, int? Skip, int? Take);
