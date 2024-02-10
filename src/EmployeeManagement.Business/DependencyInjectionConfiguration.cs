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
    }
}
