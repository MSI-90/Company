using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace Company;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Compani, CompanyDto>()
            .ForMember(c => c.FullAddress,
                opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));

        CreateMap<Employee, EmployeeDto>();

        CreateMap<CompanyForCreationDto, Compani>();

        CreateMap<EmployeeForCreationDto, Employee>();
    }
}
