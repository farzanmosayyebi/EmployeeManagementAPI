using EmployeeManagement.Common.DTOs.Employee;
using EmployeeManagement.Common.DTOs.Team;

namespace EmployeeManagement.Common.Interfaces;

public interface ITeamService
{
    Task<int> CreateTeamAsync(TeamCreate teamCreate);
    Task<TeamGet> GetTeamAsync(int Id);
    Task<List<TeamGet>> FilterTeamsAsync(TeamFilter teamFilter);
    Task AddEmployeeAsync(int teamId, int employeeId);
    Task RemoveEmployeeAsync(int teamId, int employeeId);
    Task UpdateTeamAsync(TeamUpdate teamUpdate);
    Task DeleteTeamAsync(TeamDelete teamDelete);
}
