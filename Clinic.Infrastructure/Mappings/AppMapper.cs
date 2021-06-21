using AutoMapper;
using Clinic.Core.DTOs.AppUser;
using Clinic.Core.DTOs.Employee;
using Clinic.Core.DTOs.Person;
using Clinic.Core.Entities;

namespace Clinic.Infrastructure.Mappings
{
    public class AppMapper : Profile
    {
        public AppMapper()
        {
            CreateMap<Employee, EmployeeListDTO>()
                .ForMember(dest => dest.FullName,
                           opt => opt.MapFrom(src => src.Person.Names + " " + src.Person.Surnames))
                .ForMember(dest => dest.Identification,
                           opt => opt.MapFrom(src => src.Person.Identification))
                .ForMember(dest => dest.EntityStatus,
                           opt => opt.MapFrom(src => src.AppUser.EntityStatus))
                .ReverseMap();

            CreateMap<PersonDTO, Person>()
                .ReverseMap();

            CreateMap<AppUserDTO, AppUser>()
                .ReverseMap();

            CreateMap<EmployeeCreateDTO, Employee>()
                .ForMember(dest => dest.AppUser,
                           opt => opt.MapFrom(src => src.AppUser))
                .ForMember(dest => dest.EmployeeRole,
                           opt => opt.MapFrom(src => src.Employee.EmployeeRole))
                .ForMember(dest => dest.Person,
                           opt => opt.MapFrom(src => src.Person))
                .ReverseMap();


        }
    }
}
