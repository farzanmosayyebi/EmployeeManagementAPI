using EmployeeManagement.Common.DTOs.Employee;

namespace EmployeeManagement.Common.Interfaces;

public interface IEmployeeService
{
    Task<int> CreateEmployeeAsync(EmployeeCreate employeeCreate);
    Task<EmployeeDetails> GetEmployeeAsync(int id);
    Task<List<EmployeeList>> FilterEmployeesAsync(EmployeeFilter employeeFilter);
    Task UpdateEmployeeAsync(EmployeeUpdate employeeUpdate);
    Task DeleteEmployeeAsync(EmployeeDelete employeeDelete);
}
