using Clinic.Core.DTOs.AppUser;
using Clinic.Core.DTOs.Person;
using Clinic.Core.Enumerations;
using Microsoft.AspNetCore.Http;

namespace Clinic.Core.DTOs.Employee
{
    public class EmployeeCreateDTO
    {
        public EmployeeRole EmployeeRole { get; set; }
        public AppUserCreateDTO AppUser { get; set; }
        public PersonCreateDTO Person { get; set; }
        public IFormFile Image { get; set; }
    }
}
