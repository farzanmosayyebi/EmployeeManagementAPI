using EmployeeManagement.Common.DTOs.Employee;
using EmployeeManagement.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateEmployee(EmployeeCreate employeeCreate)
    {
        int id = await _employeeService.CreateEmployeeAsync(employeeCreate);
        return Ok(id);
    }

    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> GetEmployee(int id)
    {
        EmployeeDetails employee = await _employeeService.GetEmployeeAsync(id);
        return Ok(employee);
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> FilterEmployees([FromQuery]EmployeeFilter employeeFilter)
    {
        List<EmployeeList> employees = await _employeeService.FilterEmployeesAsync(employeeFilter);
        return Ok(employees);
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateEmployee(EmployeeUpdate employeeUpdate)
    {
        await _employeeService.UpdateEmployeeAsync(employeeUpdate);
        return Ok();
    }

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> DeleteEmployee(EmployeeDelete employeeDelete)
    {
        await _employeeService.DeleteEmployeeAsync(employeeDelete);
        return Ok();
    }
}
