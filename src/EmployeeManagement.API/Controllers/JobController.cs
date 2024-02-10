using EmployeeManagement.Common.DTOs.Job;
using EmployeeManagement.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers;

[ApiController]
[Route("[controller]")]
public class JobController : ControllerBase
{
    private readonly IJobService _jobService;
    public JobController(IJobService jobService)
    {
        _jobService = jobService;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateJob(JobCreate jobCreate)
    {
        var id = await _jobService.CreateJobAsync(jobCreate);

        return Ok(id);
    }

    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> GetJob(int id)
    {
        var job = await _jobService.GetJobAsync(id);
        return Ok(job);
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> GetAllJobs()
    {
        var jobList = await _jobService.GetAllJobsAsync();
        return Ok(jobList);
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateJob(JobUpdate jobUpdate)
    {
        await _jobService.UpdateJobAsync(jobUpdate);
        return Ok();
    }

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> DeleteJob(JobDelete jobDelete)
    {
        await _jobService.DeleteJobAsync(jobDelete);
        return Ok();
    }

}
