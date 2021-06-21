using Clinic.Core.DTOs.AppUser;
using Clinic.Core.DTOs.Person;
using Clinic.Core.Enumerations;

namespace Clinic.Core.DTOs.Employee
{
    public class EmployeeCreateDTO
    {
        public AppUserCreateDTO AppUser { get; set; }
        public PersonDTO Person { get; set; }
        public EmployeeRole EmployeeRole { get; set; }
    }
}
