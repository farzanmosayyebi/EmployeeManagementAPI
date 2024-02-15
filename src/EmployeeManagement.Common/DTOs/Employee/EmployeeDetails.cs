using EmployeeManagement.Common.DTOs.Address;
using EmployeeManagement.Common.DTOs.Job;
using EmployeeManagement.Common.DTOs.Team;

//todo: add Teams
namespace EmployeeManagement.Common.DTOs.Employee;

public record EmployeeDetails(int Id, string FirstName, string LastName, AddressGet Address, JobGet Job, List<TeamGet> Teams);
