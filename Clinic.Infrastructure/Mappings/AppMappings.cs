using AutoMapper;
using Clinic.Core.DTOs.AppUser;
using Clinic.Core.DTOs.Employee;
using Clinic.Core.DTOs.Medic;
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
            CreateMedicMaps();
            CreateMedicalSpecialtiesMaps();
        }

        private void CreateMedicMaps()
        {
            CreateMap<Medic, MedicListDTO>()
                .ForMember(dest => dest.Id,
                           opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Identification,
                           opt => opt.MapFrom(src => src.Employee.Person.Identification))
                .ForMember(dest => dest.FullName,
                           opt => opt.MapFrom(src => $"{src.Employee.Person.Names} {src.Employee.Person.Surnames}"))
                .ForMember(dest => dest.MedicalSpecialtyName,
                           opt => opt.MapFrom(src => src.MedicalSpecialty.Name))
                .ReverseMap();

            CreateMap<Medic, MedicDisplayPendingForUpdateDTO>()
                .ForMember(dest => dest.EmployeeId,
                           opt => opt.MapFrom(src => src.Employee.Id))
                .ForMember(dest => dest.Identification,
                           opt => opt.MapFrom(src => src.Employee.Person.Identification))
                .ForMember(dest => dest.Names,
                           opt => opt.MapFrom(src => src.Employee.Person.Names))
                .ForMember(dest => dest.Surnames,
                           opt => opt.MapFrom(src => src.Employee.Person.Surnames))
                .ReverseMap();

            CreateMap<Employee, MedicPendingUpdateDTO>()
                .ForMember(dest => dest.Names,
                           opt => opt.MapFrom(src => src.Person.Names))
                .ForMember(dest => dest.Surnames,
                           opt => opt.MapFrom(src => src.Person.Surnames))
                .ForMember(dest => dest.Identification,
                           opt => opt.MapFrom(src => src.Person.Identification))
                .ForMember(dest => dest.IdEmployee,
                           opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
        }

        private void CreateMedicalSpecialtiesMaps()
        {
            CreateMap<MedicalSpecialty, MedicalSpecialtyListDTO>()
                .ReverseMap();
        }

        private void CreatePersonMaps()
        {
            CreateMap<PersonDTO, Person>()
               .ReverseMap();

            CreateMap<PersonUpdateDTO, Person>()
                .ReverseMap();

            CreateMap<PersonCreateDTO, Person>()
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

            CreateMap<EmployeeUpdateDTO, Employee>()
                .ForMember(dest => dest.Person,
                           opt => opt.MapFrom(src => src.Person))
                .ReverseMap();

            CreateMap<Employee, EmployeeListDTO>()
                .ForMember(dest => dest.FullName,
                           opt => opt.MapFrom(src => src.Person.Names + " " + src.Person.Surnames))
                .ForMember(dest => dest.Identification,
                           opt => opt.MapFrom(src => src.Person.Identification))
                .ForMember(dest => dest.EmployeeStatus,
                           opt => opt.MapFrom(src => src.EmployeeStatus))
                .ReverseMap();
        }
    }
}
