using AutoMapper;
using EmployeeManagement.Business.Validation.Employee;
using EmployeeManagement.Common.DTOs.Employee;
using EmployeeManagement.Common.Interfaces;
using EmployeeManagement.Common.Models;
using FluentValidation;
using System.Linq.Expressions;

namespace EmployeeManagement.Business.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IGenericRepository<Employee> _employeeRepository;
    private readonly IGenericRepository<Address> _addressRepository;
    private readonly IGenericRepository<Job> _jobRepository;
    private readonly IMapper _mapper;

    private readonly EmployeeCreateValidator _createValidator;
    private readonly EmployeeUpdateValidator _updateValidator;
    private readonly EmployeeFilterValidator _filterValidator;

    public EmployeeService(IGenericRepository<Employee> employeeRepository, IGenericRepository<Address> addressrepository, IGenericRepository<Job> jobRepository, IMapper mapper
                           , EmployeeCreateValidator createValidator, EmployeeUpdateValidator updateValidator, EmployeeFilterValidator filterValidator)
    {
        _employeeRepository = employeeRepository;
        _addressRepository = addressrepository;
        _jobRepository = jobRepository;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<int> CreateEmployeeAsync(EmployeeCreate employeeCreate)
    {
        await _createValidator.ValidateAndThrowAsync(employeeCreate);

        var entity = _mapper.Map<Employee>(employeeCreate);

        var address = await _addressRepository.GetByIdAsync(employeeCreate.AddresId);
        var job = await _jobRepository.GetByIdAsync(employeeCreate.JobId);

        entity.Address = address;
        entity.Job = job;

        int id = await _employeeRepository.InsertAsync(entity);
        await _employeeRepository.SaveChangesAsync();
        
        return id;
    }

    public async Task DeleteEmployeeAsync(EmployeeDelete employeeDelete)
    {
        var entity = await _employeeRepository.GetByIdAsync(employeeDelete.Id);
        _employeeRepository.Delete(entity);
        await _employeeRepository.SaveChangesAsync();
    }

    public async Task<List<EmployeeList>> FilterEmployeesAsync(EmployeeFilter employeeFilter)
    {
        await _filterValidator.ValidateAndThrowAsync(employeeFilter);

        Expression<Func<Employee, bool>> firstNameFilter = employee => employeeFilter.FirstName == null ? true
            : employee.FirstName.StartsWith(employeeFilter.FirstName);

        Expression<Func<Employee, bool>> lastNameFilter = employee => employeeFilter.LastName == null ? true
            : employee.LastName.StartsWith(employeeFilter.LastName);
        
        Expression<Func<Employee, bool>> addressFilter = employee => employeeFilter.AddressId == null ? true
            : employee.Address.Id == employeeFilter.AddressId;
        
        Expression<Func<Employee, bool>> jobFilter = employee => employeeFilter.JobId == null ? true
            : employee.Job.Id == employeeFilter.JobId;
        
        Expression<Func<Employee, bool>> teamFilter = employee => employeeFilter.TeamId == null ? true
            : employee.Teams.Any(t => t.Id == employeeFilter.TeamId);

        Expression<Func<Employee, bool>>[] filters = [firstNameFilter, lastNameFilter, addressFilter, jobFilter, teamFilter];

        var entityList = await _employeeRepository.GetFilteredAsync(filters, employeeFilter.Skip, employeeFilter.Take);

        return _mapper.Map<List<EmployeeList>>(entityList);
    }

    public async Task<EmployeeDetails> GetEmployeeAsync(int id)
    {
        var entity = await _employeeRepository.GetByIdAsync(id, e => e.Address, e => e.Job, e => e.Teams);
        return _mapper.Map<EmployeeDetails>(entity);
    }

    public async Task UpdateEmployeeAsync(EmployeeUpdate employeeUpdate)
    {
        await _updateValidator.ValidateAndThrowAsync(employeeUpdate);

        var entity = _mapper.Map<Employee>(employeeUpdate);

        _employeeRepository.Update(entity);
        await _employeeRepository.SaveChangesAsync();
    }
}
