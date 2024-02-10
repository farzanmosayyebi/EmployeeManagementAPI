using EmployeeManagement.Common.DTOs.Job;

namespace EmployeeManagement.Common.Interfaces;
public interface IJobService
{
    Task<int> CreateJobAsync(JobCreate jobCreate);
    Task<JobGet> GetJobAsync(int id);
    Task<List<JobGet>> GetAllJobsAsync();
    Task DeleteJobAsync(JobDelete jobDelete);
    Task UpdateJobAsync(JobUpdate jobUpdate);
}
