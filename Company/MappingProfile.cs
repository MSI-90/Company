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

        CreateMap<CompanyForUpdateDto, Compani>();
        CreateMap<CompanyForCreationDto, Compani>();

        CreateMap<Employee, EmployeeDto>();
        CreateMap<EmployeeForCreationDto, Employee>();
        CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();
        CreateMap<UserForRegistrationDto, User>();
    }
}
