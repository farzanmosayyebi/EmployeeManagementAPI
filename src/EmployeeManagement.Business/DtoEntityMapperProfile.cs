using AutoMapper;
using EmployeeManagement.Common.Models;
using EmployeeManagement.Common.DTOs.Address;
using EmployeeManagement.Common.DTOs.Job;
using EmployeeManagement.Common.DTOs.Employee;

namespace EmployeeManagement.Business;

public class DtoEntityMapperProfile : Profile
{
    public DtoEntityMapperProfile()
    {
        CreateMap<AddressCreate, Address>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<AddressUpdate, Address>();
        CreateMap<Address, AddressGet>();

        CreateMap<JobCreate, Job>()
            .ForMember(j => j.Id, opt => opt.Ignore());
        CreateMap<JobUpdate, Job>();
        CreateMap<Job, JobGet>();

        CreateMap<EmployeeCreate, Employee>()
            .ForMember(e => e.Id, opt => opt.Ignore())
            .ForMember(e => e.Job, opt => opt.Ignore())
            .ForMember(e => e.Teams, opt => opt.Ignore());

        CreateMap<EmployeeUpdate, Employee>()
            .ForMember(e => e.Job, opt => opt.Ignore())
            .ForMember(e => e.Teams, opt => opt.Ignore());

        CreateMap<Employee, EmployeeDetails>() //todo: add teams
            .ForMember(e => e.Id, opt => opt.Ignore())
            .ForMember(e => e.Address, opt => opt.Ignore())
            .ForMember(e => e.Job, opt => opt.Ignore());

        CreateMap<Employee, EmployeeList>();    

    }
}
