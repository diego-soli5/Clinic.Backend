using Clinic.Core.DTOs.AppUser;
using Clinic.Core.DTOs.Person;
using Clinic.Core.Enumerations;
using Microsoft.AspNetCore.Http;

namespace Clinic.Core.DTOs.Employee
{
    public class EmployeeCreateDTO
    {
        public AppUserCreateDTO AppUser { get; set; }
        public PersonDTO Person { get; set; }
        public EmployeeRole EmployeeRole { get; set; }
        public IFormFile Image { get; set; }
    }
}
