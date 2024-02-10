using AutoMapper;
using EmployeeManagement.Common.Models;
using EmployeeManagement.Common.DTOs.Address;
using EmployeeManagement.Common.DTOs.Job;

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

    }
}
