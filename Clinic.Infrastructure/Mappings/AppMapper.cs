using AutoMapper;
using Clinic.Core.DTOs.Employee;
using Clinic.Core.Entities;

namespace Clinic.Infrastructure.Mappings
{
    public class AppMapper : Profile
    {
        public AppMapper()
        {
            CreateMap<Employee, EmployeeDTO>()
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
