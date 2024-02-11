using EmployeeManagement.Common.DTOs.Employee;

namespace EmployeeManagement.Common.DTOs.Team;

public record TeamGet(int Id, string Name, List<EmployeeList> Employees);
