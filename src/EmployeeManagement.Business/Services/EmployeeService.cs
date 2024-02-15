using AutoMapper;
using EmployeeManagement.Business.Exceptions;
using EmployeeManagement.Business.Validation.Employee;
using EmployeeManagement.Common.DTOs.Employee;
using EmployeeManagement.Common.Interfaces;
using EmployeeManagement.Common.Models;
using FluentValidation;
using System.Linq.Expressions;
using System.Reflection;

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
        _filterValidator = filterValidator;
    }

    public async Task<int> CreateEmployeeAsync(EmployeeCreate employeeCreate)
    {
        await _createValidator.ValidateAndThrowAsync(employeeCreate);

        var entity = _mapper.Map<Employee>(employeeCreate);

        Address address = await _addressRepository.GetByIdAsync(employeeCreate.AddressId)
            ?? throw new ItemNotFoundException(typeof(Address), employeeCreate.AddressId);

        Job job = await _jobRepository.GetByIdAsync(employeeCreate.JobId)
            ?? throw new ItemNotFoundException(typeof(Job), employeeCreate.JobId);

        entity.Address = address;
        entity.Job = job;

        int id = await _employeeRepository.InsertAsync(entity);
        await _employeeRepository.SaveChangesAsync();
        
        return entity.Id;
    }

    public async Task DeleteEmployeeAsync(EmployeeDelete employeeDelete)
    {
        var entity = await _employeeRepository.GetByIdAsync(employeeDelete.Id)
            ?? throw new ItemNotFoundException(typeof(Employee), employeeDelete.Id);

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
        var entity = await _employeeRepository.GetByIdAsync(id, e => e.Address, e => e.Job, e => e.Teams)
            ?? throw new ItemNotFoundException(typeof(Employee), id);

        return _mapper.Map<EmployeeDetails>(entity);
    }

    public async Task UpdateEmployeeAsync(EmployeeUpdate employeeUpdate)
    {
        await _updateValidator.ValidateAndThrowAsync(employeeUpdate);

        Employee existingEntity = await _employeeRepository.GetByIdAsync(employeeUpdate.Id)
            ?? throw new ItemNotFoundException(typeof(Employee), employeeUpdate.Id);

        Employee entity = _mapper.Map<Employee>(employeeUpdate);
        
        Address address = await _addressRepository.GetByIdAsync(employeeUpdate.AddressId)
            ?? throw new ItemNotFoundException(typeof(Address), employeeUpdate.AddressId);

        Job job = await _jobRepository.GetByIdAsync(employeeUpdate.JobId)
            ?? throw new ItemNotFoundException(typeof(Job), employeeUpdate.JobId);

        entity.Address = address;
        entity.Job = job;

        _mapper.Map(entity, existingEntity);

        existingEntity.Address = address;
        existingEntity.Job = job;

        _employeeRepository.Update(existingEntity);
        await _employeeRepository.SaveChangesAsync();
    }
}
