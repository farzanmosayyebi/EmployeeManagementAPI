using EmployeeManagement.Common.DTOs.Employee;
using EmployeeManagement.Common.DTOs.Team;
using EmployeeManagement.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TeamController : ControllerBase
{
    private readonly ITeamService _teamService;

    public TeamController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateTeam(TeamCreate teamCreate)
    {
        int id = await _teamService.CreateTeamAsync(teamCreate);
        return Ok(id);
    }

    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> GetTeam(int id)
    {
        var team = await _teamService.GetTeamAsync(id);
        return Ok(team);
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> FilterTeams([FromQuery]TeamFilter teamFilter)
    {
        List<TeamGet> teams = await _teamService.FilterTeamsAsync(teamFilter);
        return Ok(teams);
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateTeam(TeamUpdate teamUpdate)
    {
        await _teamService.UpdateTeamAsync(teamUpdate);
        return Ok();
    }

    [HttpPut]
    [Route("Update/Add")]
    public async Task<IActionResult> AddEmployee(int teamId, int employeeId)
    {
        await _teamService.AddEmployeeAsync(teamId, employeeId);
        return Ok();
    }

    [HttpPut]
    [Route("Update/Remove")]
    public async Task<IActionResult> RemoveEmployee(int teamId, int employeeId)
    {
        await _teamService.RemoveEmployeeAsync(teamId, employeeId);
        return Ok();
    }

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> DeleteTeam(TeamDelete teamDelete)
    {
        await _teamService.DeleteTeamAsync(teamDelete);
        return Ok();
    }


}
