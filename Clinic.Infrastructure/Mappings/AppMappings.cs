using AutoMapper;
using Clinic.Core.DTOs.AppUser;
using Clinic.Core.DTOs.Employee;
using Clinic.Core.DTOs.Person;
using Clinic.Core.Entities;

namespace Clinic.Infrastructure.Mappings
{
    public class AppMappings : Profile
    {
        public AppMappings()
        {
            CreateEmployeeMaps();
            CreatePersonMaps();
            CreateAppUserMaps();
        }

        private void CreatePersonMaps()
        {
            CreateMap<PersonDTO, Person>()
               .ReverseMap();
        }

        private void CreateAppUserMaps()
        {
            CreateMap<AppUserCreateDTO, AppUser>()
                .ReverseMap();

            CreateMap<AppUser, AppUserDTO>()
                .ReverseMap();
        }

        private void CreateEmployeeMaps()
        {
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(dest => dest.AppUser,
                           opt => opt.MapFrom(src => src.AppUser))
                .ForMember(dest => dest.Person,
                           opt => opt.MapFrom(src => src.Person));

            CreateMap<EmployeeCreateDTO, Employee>()
                .ForMember(dest => dest.AppUser,
                           opt => opt.MapFrom(src => src.AppUser))
                .ForMember(dest => dest.EmployeeRole,
                           opt => opt.MapFrom(src => src.EmployeeRole))
                .ForMember(dest => dest.Person,
                           opt => opt.MapFrom(src => src.Person))
                .ReverseMap();

            CreateMap<Employee, EmployeeListDTO>()
                .ForMember(dest => dest.FullName,
                           opt => opt.MapFrom(src => src.Person.Names + " " + src.Person.Surnames))
                .ForMember(dest => dest.Identification,
                           opt => opt.MapFrom(src => src.Person.Identification))
                .ForMember(dest => dest.EntityStatus,
                           opt => opt.MapFrom(src => src.AppUser.EntityStatus))
                .ReverseMap();
        }
    }
}
