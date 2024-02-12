using AutoMapper;
using EmployeeManagement.Business.Validation.Team;
using EmployeeManagement.Common.DTOs.Employee;
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
        int id = await _teamRepository.InsertAsync(team);
        await _teamRepository.SaveChangesAsync();
        return id;
    }

    public async Task AddEmployeesAsync(int teamId, List<int> employeeIds)
    {
        Expression<Func<Employee, bool>> employeeFilter = employee => employeeIds.Contains(employee.Id);

        Team team = await _teamRepository.GetByIdAsync(teamId, t => t.Employees);


        List<Employee> employeeEntities = await _employeeRepository.GetFilteredAsync([employeeFilter], null, null);
        //employeeIds.ForEach(async id => employeeEntities.Add(await _employeeRepository.GetByIdAsync(id)));

        team.Employees.AddRange(employeeEntities);
        _teamRepository.Update(team);
        
        await _teamRepository.SaveChangesAsync();
    }

    public async Task DeleteTeamAsync(TeamDelete teamDelete)
    {
        Team team = await _teamRepository.GetByIdAsync(teamDelete.Id);
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

        List<Team> teams = await _teamRepository.GetFilteredAsync(filters, teamFilter.Skip, teamFilter.Take);

        return _mapper.Map<List<TeamGet>>(teams);
    }

    public async Task<TeamGet> GetTeamAsync(int Id)
    {
        Team team = await _teamRepository.GetByIdAsync(Id, t => t.Employees);
        
        return _mapper.Map<TeamGet>(team);
    }

    public async Task RemoveEmployeesAsync(int teamId, List<int> employeeIds)
    {
        Expression<Func<Employee, bool>> employeeFilter = employee => employeeIds.Contains(employee.Id);
        Team team = await _teamRepository.GetByIdAsync(teamId, t => t.Employees);

        List<Employee> employeeEntities = await _employeeRepository.GetFilteredAsync([employeeFilter], null, null);
        employeeEntities.ForEach(e => team.Employees.Remove(e));

        _teamRepository.Update(team);
        await _teamRepository.SaveChangesAsync();
    }

    public async Task UpdateTeamAsync(TeamUpdate teamUpdate)
    {
        await _updateValidator.ValidateAndThrowAsync(teamUpdate);

        Team team = _mapper.Map<Team>(teamUpdate);

        _teamRepository.Update(team);
        await _teamRepository.SaveChangesAsync();
    }
}
