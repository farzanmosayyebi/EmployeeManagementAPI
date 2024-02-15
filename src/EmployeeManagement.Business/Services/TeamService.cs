using AutoMapper;
using EmployeeManagement.Business.Exceptions;
using EmployeeManagement.Business.Validation.Team;
using EmployeeManagement.Common.DTOs.Team;
using EmployeeManagement.Common.Interfaces;
using EmployeeManagement.Common.Models;
using FluentValidation;
using System.Linq.Expressions;

namespace EmployeeManagement.Business.Services;

internal class TeamService : ITeamService
{
    private readonly IGenericRepository<Team> _teamRepository;
    private readonly IGenericRepository<Employee> _employeeRepository;
    private readonly IMapper _mapper;

    private readonly TeamCreateValidator _createValidator;
    private readonly TeamUpdateValidator _updateValidator;
    private readonly TeamFilterValidator _filterValidator;

    public TeamService(IGenericRepository<Team> teamRepository, IGenericRepository<Employee> employeeRepository, IMapper mapper,
                        TeamCreateValidator createValidator, TeamUpdateValidator updateValidator, TeamFilterValidator filterValidator)
    {
        _teamRepository = teamRepository;
        _employeeRepository = employeeRepository;
        _mapper = mapper;

        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _filterValidator = filterValidator;
    }

    public async Task<int> CreateTeamAsync(TeamCreate teamCreate)
    {
        await _createValidator.ValidateAndThrowAsync(teamCreate);

        Team team = _mapper.Map<Team>(teamCreate);
        await _teamRepository.InsertAsync(team);
        await _teamRepository.SaveChangesAsync();
        return team.Id;
    }

    public async Task AddEmployeeAsync(int teamId, int employeeId)
    {
        Team team = await _teamRepository.GetByIdAsync(teamId, t => t.Employees)
            ?? throw new ItemNotFoundException(typeof(Team), teamId);

        if (team.Employees.Any(e => e.Id == employeeId))
            throw new InvalidAddOperationException(typeof(Employee), employeeId);

        Employee employee = await _employeeRepository.GetByIdAsync(employeeId)
            ?? throw new ItemNotFoundException(typeof(Employee), employeeId);

        team.Employees.Add(employee);

        _teamRepository.Update(team);
        await _teamRepository.SaveChangesAsync();
    }

    public async Task DeleteTeamAsync(TeamDelete teamDelete)
    {
        Team team = await _teamRepository.GetByIdAsync(teamDelete.Id, team => team.Employees)
            ?? throw new ItemNotFoundException(typeof(Team), teamDelete.Id);

        if (team.Employees.Count > 0)
            throw new DependentEmployeesExistException(team.Employees);

        _teamRepository.Delete(team);
        await _teamRepository.SaveChangesAsync();
    }

    public async Task<List<TeamGet>> FilterTeamsAsync(TeamFilter teamFilter)
    {
        await _filterValidator.ValidateAndThrowAsync(teamFilter);

        Expression<Func<Team, bool>> nameFilter = t => teamFilter.Name == null ? true
            : t.Name.StartsWith(teamFilter.Name);
        
        Expression<Func<Team, bool>> employeeFilter = t => teamFilter.EmployeeId == null ? true 
            : t.Employees.Any(e => e.Id == teamFilter.EmployeeId);

        Expression<Func<Team, bool>>[] filters = [nameFilter, employeeFilter];

        List<Team> teams = await _teamRepository.GetFilteredAsync(filters, teamFilter.Skip, teamFilter.Take, team => team.Employees);

        return _mapper.Map<List<TeamGet>>(teams);
    }

    public async Task<TeamGet> GetTeamAsync(int id)
    {
        Team team = await _teamRepository.GetByIdAsync(id, t => t.Employees)
            ?? throw new ItemNotFoundException(typeof(Team), id);
        
        return _mapper.Map<TeamGet>(team);
    }

    public async Task RemoveEmployeeAsync(int teamId, int employeeId)
    {
        Team team = await _teamRepository.GetByIdAsync(teamId, t => t.Employees)
            ?? throw new ItemNotFoundException(typeof(Team), teamId);

        if (!team.Employees.Any(e => e.Id == employeeId))
            throw new InvalidRemoveOperationException(typeof(Employee), employeeId);

        Employee employee = await _employeeRepository.GetByIdAsync(employeeId)
            ?? throw new ItemNotFoundException(typeof(Employee), employeeId);

        team.Employees.Remove(employee);

        _teamRepository.Update(team);
        await _teamRepository.SaveChangesAsync();
    }

    public async Task UpdateTeamAsync(TeamUpdate teamUpdate)
    {
        await _updateValidator.ValidateAndThrowAsync(teamUpdate);

        Team existingEntity = await _teamRepository.GetByIdAsync(teamUpdate.Id)
            ?? throw new ItemNotFoundException(typeof(Team), teamUpdate.Id);

        Team entity = _mapper.Map<Team>(teamUpdate);
        _mapper.Map(entity, existingEntity);

        _teamRepository.Update(existingEntity);
        await _teamRepository.SaveChangesAsync();
    }
}
