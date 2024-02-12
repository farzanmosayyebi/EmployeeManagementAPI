using EmployeeManagement.Business.Validation.Address;
using EmployeeManagement.Business.Validation.Employee;
using EmployeeManagement.Business.Validation.Job;
using EmployeeManagement.Business.Validation.Team;
using EmployeeManagement.Business.Services;

using EmployeeManagement.Common.Interfaces;

using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement.Business;

public class DependencyInjectionConfiguration
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DtoEntityMapperProfile));

        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IJobService, JobService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<ITeamService, TeamService>();

        services.AddScoped<AddressCreateValidator>();
        services.AddScoped<AddressUpdateValidator>();

        services.AddScoped<EmployeeCreateValidator>();
        services.AddScoped<EmployeeUpdateValidator>();
        services.AddScoped<EmployeeFilterValidator>();

        services.AddScoped<JobCreateValidator>();
        services.AddScoped<JobUpdateValidator>();

        services.AddScoped<TeamCreateValidator>();
        services.AddScoped<TeamUpdateValidator>();
        services.AddScoped<TeamFilterValidator>();
    }
}
